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
            Session["member"] = id;
            return View(m);
        }
        [HttpPost]
        public ActionResult receiveOrder(string[] cartID)
        {
            var cod = db.Cart_OrderDetail;
            string member = Session["member"] as string;
            List<Cart_OrderDetail> m = (from p in cod
                                        where p.mbrID == member
                                        where p.odrID == null
                                        orderby p.Specification.Product.selID
                                        select p).ToList();
            foreach (string cartIndex in cartID)
            {
                m[Int32.Parse(cartIndex)].odrID = "odrtest001";
                db.Entry(m[Int32.Parse(cartIndex)]).State = EntityState.Modified;
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