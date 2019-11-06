using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using System.Data;
using System.Data.SqlClient;

namespace DeWay.Controllers
{
    public class SellerCertificationController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: SellerCertification
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult IDNumber()
        {

            return View();
        }
        [HttpPost]
        public ActionResult IDNumber(Seller id)
        {
            id.selID = db.Database.SqlQuery<string>("select dbo.GetSellerID()").FirstOrDefault();
            db.Seller.Add(id);
            db.SaveChanges();
            return View(id);
        }

        public ActionResult GUINumber()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GUINumber(Seller gui)
        {
            gui.selID = db.Database.SqlQuery<string>("Select dbo.GetSellerID()").FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.Seller.Add(gui);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gui);
        }
    }
}