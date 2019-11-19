using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;


namespace Project.Controllers
{
    public class CartController : Controller
    {
        //TODO : 假如商品下單成功，將庫存數 減去 商品下單數
        //反之，假如訂單被取消，則加回相對應的庫存數
        //TODO : 購物車內可以選擇購買數
        shopDBEntities db = new shopDBEntities();
        public List<Order> test(string mbrID)
        {
            List<string> odrIDList = db.Cart_OrderDetail.Where(m => m.mbrID == mbrID).Select(m => m.odrID).ToList();
            var result = db.Order.Where(o => odrIDList.Contains(o.odrID)).ToList();


            return result;
        }
        public ActionResult myCart(string id = "mbr0000001")
        {
            var cod = db.Cart_OrderDetail;
            var m = from p in cod
                    where p.mbrID == id
                    where p.odrID == null
                    select p;
            ViewBag.memberName = db.Member.Find(id).mbrName;
            //m.Select((item,Product)=>item.)
            Session["member"] = id;
            return View(m);
        }
        [HttpPost]
        public ActionResult receiveOrder(string[] cartID, string[] shipSelect, Order order) //提交訂單
        {
            try
            {
                var cod = db.Cart_OrderDetail;
                string member = Session["member"] as string;
                List<Cart_OrderDetail> m = (from p in cod
                                            where p.mbrID == member
                                            where p.odrID == null
                                            orderby p.Specification.Product.selID
                                            select p).ToList();
                string availableOdrId = db.Database.SqlQuery<string>("Select dbo.GetOrderID()").FirstOrDefault();
                for (int i = 0; i < cartID.Length; i++)
                {
                    if (cartID[i] != "false")
                    {
                        string currentCartID = cartID[i];
                        string selectedShipMethod = shipSelect[i];
                        Cart_OrderDetail currentCart = m[i];
                        currentCart.odrID = availableOdrId;
                        currentCart.shpID = db.Shipper.Where(t => t.shpMethod == selectedShipMethod).FirstOrDefault().shpID;
                        //db.Entry(currentCart).State = EntityState.Modified;
                    }
                }
                //寫入訂單資料表
                order.odrID = availableOdrId;
                order.traceNumber = "332";//物流單號 為not NULL 欄位 在此隨便填
                order.pmtID = "pmt0000001";//付款方式
                order.odrStatusID = "ods0000001";//訂單狀態
                order.odrDate = DateTime.Now;//訂單成立時間
                db.Order.Add(order);

                db.SaveChanges();
                return View("myCart", m);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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