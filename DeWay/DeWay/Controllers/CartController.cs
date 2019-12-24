using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using DeWay.App_Code;
using System.Data.Entity.Infrastructure;

namespace Project.Controllers
{
    public class CartController : Controller
    {
        //TODO :假如訂單被取消，加回相對應的庫存數
        //TODO : 每筆訂單都有各自給賣家的話
        shopDBEntities db = new shopDBEntities();

        [HttpPost]
        public ActionResult AddToMyCart(string spcID, int quantity = 1)
        {   //TODO:加入 請訪客登入
            //把memberID加入MBRID

            string mbrID = (string)Session["memberID"]; //利用SESSION存取
            if (mbrID is null)
            {
                string js = "alert('請先登入會員才可加入購物車')";
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return JavaScript(js);
            }


            Cart_OrderDetail cod;
            //檢查是否已經加入購物車
            var getCartItem = db.Cart_OrderDetail.Where(m => m.mbrID == mbrID)
                .Where(m => m.spcID == spcID)
                .Where(m => m.odrID == null);
            int inCartCount = getCartItem.Count();
            if (inCartCount > 0)
            {
                cod = getCartItem.FirstOrDefault();
                cod.Quantity = quantity;
            }
            else
            {
                cod = new Cart_OrderDetail()
                {
                    cartID = db.Database.SqlQuery<string>("Select dbo.GetCartID()").FirstOrDefault(),
                    mbrID = mbrID,
                    Quantity = quantity,
                    spcID = spcID,
                    usedPoints = 0,
                    Discount = 0,
                    pdtPrice = 0,
                    shpID = "shp0000001" //預設運送方式
                };
                db.Cart_OrderDetail.Add(cod);
            }

            try
            {
                db.SaveChanges();
                return JavaScript("alert('成功新增至購物車')");
            }
            catch (Exception e)
            {
                return JavaScript($"alert('失敗了，請再嘗試一次')");
            }
        }
        public ActionResult MyCart()
        {
            string id = (string)Session["memberID"];
            if (id == null) //未登入，倒回登入頁面
                return RedirectToAction("login", "login");
            var cod = db.Cart_OrderDetail;
            var m = from p in cod
                    where p.mbrID == id
                    where p.odrID == null
                    select p;
            //如果商品售完，直接從購物車移除
            var soldOutItem = m.Where(q => q.Specification.Stock == 0).ToList();
            if (soldOutItem.Count != 0)
            {
                foreach (var soldOut in soldOutItem)
                {
                    db.Cart_OrderDetail.Remove(soldOut);
                }
                db.SaveChanges();
                ViewBag.boolSoldOut = true;
            }

            ViewBag.memberName = db.Member.Find(id).mbrName;
            return View(m);
        }


        [HttpPost]
        public ActionResult receiveOrder(string[] cartID, string[] shipSelect, string[] quantity, Order order) //提交訂單
        {
            var cod = db.Cart_OrderDetail;
            string memberID = Session["memberID"] as string;
            List<Cart_OrderDetail> m = (from p in cod
                                        where p.mbrID == memberID
                                        where p.odrID == null
                                        orderby p.Specification.Product.selID
                                        select p).ToList();
            List<string> sellerIDList = m.Where(sItem=>cartID.Contains(sItem.cartID)).Select(s => s.Specification.Product.selID).Distinct().ToList();
            //if (ModelState.IsValid != true)
            //{
            //    return View("myCart");
            //}
            using (var transaction = db.Database.BeginTransaction())
            {

                //初始化訂單
                order.traceNumber = "";                //物流單號 為not NULL 欄位 在此隨便填
                order.pmtID = "pmt0000001";            //付款方式
                order.odrStatusID = "ods0000001";      //訂單狀態
                order.odrDate = DateTime.Now;          //訂單成立時間
                order.shpPrice = 100;
                order.traceNumber = "123456";
                order.recPhone = "0911-111-111";

                foreach (var sellerID in sellerIDList)
                {
                    order.selID = sellerID;
                    order.odrID = db.Database.SqlQuery<string>("Select dbo.GetOrderID()").FirstOrDefault();

                    try
                    {
                        db.Order.Add(order);
                        db.SaveChanges();
                        db.Entry(order).State = EntityState.Detached;
                    }
                    catch (Exception d)
                    {
                        transaction.Rollback();
                        return JavaScript($"alert('失敗了，請再嘗試一次')");
                    }
                }
                //寫入COD資料
                string orderID = "";
                for (int i = 0; i < cartID.Length; i++) //依據傳入的cartID[]條件 加入cart
                {

                    if (cartID[i] != "false")
                    {
                        Cart_OrderDetail currentCart = m[i];
                        orderID = currentCart.Specification.Product.selID;
                        string selectedShipMethod = shipSelect[i];
                        string SelID = currentCart.Specification.Product.selID;
                        currentCart.odrID = db.Order.Where(od => od.selID == SelID).ToList().LastOrDefault().odrID;
                        currentCart.shpID = db.Shipper.Where(t => t.shpMethod == selectedShipMethod).FirstOrDefault().shpID;
                        currentCart.pdtPrice = currentCart.Specification.Price;   //取得商品時價
                        currentCart.Discount = currentCart.Specification.Discount;//取得商品當時打折

                        //庫存減去商品下單數
                        if ((currentCart.Specification.Stock - currentCart.Quantity) >= 0)
                        {
                            currentCart.Specification.Stock -= currentCart.Quantity;
                        }
                        else
                        {
                            return JavaScript("alert('下單量大於庫存囉')");
                        }
                    }
                }

                try
                {

                    db.SaveChanges();

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    return JavaScript($"alert({e.Entries})");
                }


                ///更新ORDER 運費 
                var odr = db.Order.Where(o => o.shpPrice == null).ToList();
                string shpID;
                foreach (var oItem in odr)
                {
                    shpID = db.Cart_OrderDetail.Where(c => c.odrID == oItem.odrID).FirstOrDefault().shpID;
                    oItem.shpPrice = db.ShipperDetail.Where(p => p.shpID == shpID).FirstOrDefault().defaultShipping;
                }
                try
                {
                    db.SaveChanges();
                    transaction.Commit();
                    return RedirectToAction("MyCart");
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    return JavaScript($"alert('失敗了，請再嘗試一次')");
                }

            }

        }
        public ActionResult Delete(string cartID, string mbrID)
        {
            var cod = db.Cart_OrderDetail;
            var p = db.Cart_OrderDetail.Find(cartID);
            cod.Remove(p);
            db.SaveChanges();
            return RedirectToAction("myCart", new { id = mbrID });
        }
    }
}