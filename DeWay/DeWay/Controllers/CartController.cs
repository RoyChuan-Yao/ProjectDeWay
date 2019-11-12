using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;


namespace Project.Controllers
{
    public class CartController : Controller
    {

        shopDBEntities db = new shopDBEntities();

        public ActionResult myCart(string id)
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
            var cod = db.Cart_OrderDetail;
            string member = Session["member"] as string;
            List<Cart_OrderDetail> m = (from p in cod
                                        where p.mbrID == member
                                        where p.odrID == null
                                        orderby p.Specification.Product.selID
                                        select p).ToList();
            foreach (string cartIndexStr in cartID) //針對cartOrderDetail資料寫入
            {
                int cartIndexInt = Int32.Parse(cartIndexStr);
                string selectedShipMethod = shipSelect[cartIndexInt];
                Cart_OrderDetail currentCart = m[cartIndexInt];
                currentCart.odrID = "odrtest001";
                currentCart.shpID = db.Shipper.Find(selectedShipMethod).shpID;
                
                db.Entry(m[cartIndexInt]).State = EntityState.Modified;
            }
            db.SaveChanges();

            return View(m);
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