using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;


namespace DeWay.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        shopDBEntities db = new shopDBEntities();
        public ActionResult Index()
        {
            var memberID = (string)Session["memberID"];
            if (memberID != null)
                ViewBag.memberName = db.Member.Find(memberID).nickName;
            return View();
        }
    }
}