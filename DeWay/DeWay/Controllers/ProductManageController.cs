using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;

namespace DeWay.Controllers
{

    public class ProductManageController : Controller
    {
        shopDBEntities db = new shopDBEntities();


        // GET: Home
        public ActionResult Index()
        {
            string mbrID = Session["memberID"].ToString();
            var seller = from s in db.Seller
                         where s.mbrID == mbrID
                         select s;
            var pro = db.Product;
            var m = from a in pro
                    where a.selID == seller.FirstOrDefault().selID
                    select a;
            Session["selID"] = m.FirstOrDefault().selID;
            if (m != null) return View(m);
            else return RedirectToAction("index", "productmanage");
        }
      
        public ActionResult CreatePdt(string selID)
        {
            @ViewBag.selID = selID;
            return View();
        }
        [HttpPost]
        public ActionResult CreatePdt(Product pdt)
        {
            DateTime dt = DateTime.Now;
            pdt.pdtDate = dt;
            db.Product.Add(pdt);
            
            db.SaveChanges();
            

            return RedirectToAction("Index", "productmanage", new { id = pdt.selID });
        }

        public ActionResult Edit(string id)
        {
            //Create+Delete
            //將TodoData裡的資料，讀出某一筆ID全部欄位資料抓出來，於進Controllers的todo裡。
            var pdt = db.Product.Where(m => m.pdtID == id).FirstOrDefault();
            return View(pdt);
        }
        [HttpPost]
        public ActionResult Edit(Product pdt)
        {
            var pdtResult = db.Product.Where(m => m.pdtID == pdt.pdtID).FirstOrDefault();
            pdtResult.pdtName = pdt.pdtName;
            pdtResult.pdtDescribe = pdt.pdtDescribe;
            pdtResult.Discontinued = pdt.Discontinued;
            db.SaveChanges();
            return RedirectToAction("index", "productmanage", new { id = pdt.selID });
        }
    }
}