using System;
using System.Collections.Generic;
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
                    select p;
            Session["member"] = id;
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