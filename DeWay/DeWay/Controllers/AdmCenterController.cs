using DeWay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeWay.Controllers
{
    public class AdmCenterController : Controller
    {
        shopDBEntities db = new shopDBEntities();
        // GET: AdmCenter
        public ActionResult Index(string id)
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
            else
            {
                var cd = db.Adm;
                var m = from p in cd
                        where p.admID == id
                        select p;
                Session["AdmID"] = id;
                return View(m);
            }
        }

    }
}