using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;

namespace DeWay.Controllers
{
    public class ProductsAdmController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: Products

        public ActionResult Index(string searchString)
        {
            var product = from m in db.Product
                          select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                product = product.Where(s => s.pdtName.Contains(searchString));
            }
            return View(product);
        }


        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //// GET: Products/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ctgID = new SelectList(db.ProductCategory, "pdtCtgID", "fstLayerID");
        //    ViewBag.selID = new SelectList(db.Seller, "selID", "selCompany");
        //    return View();
        //}

        //// POST: Products/Create
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Product product)
        //{
        //    string GetproductID = db.Database.SqlQuery<string>("Select dbo.GetProductID()").FirstOrDefault();
        //    product.pdtID = GetproductID;

        //    //if (ModelState.IsValid)
        //    //{
        //    //    db.Product.Add(product);
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}
        //    db.Product.Add(product);
        //    db.SaveChanges();

        //    ViewBag.ctgID = new SelectList(db.ProductCategory, "pdtCtgID", "fstLayerID", product.ctgID);
        //    ViewBag.selID = new SelectList(db.Seller, "selID", "selCompany", product.selID);
        //    return View(product);
        //}

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.ctgID = new SelectList(db.ProductCategory, "pdtCtgID", "fstLayerID", product.ctgID);
            ViewBag.selID = new SelectList(db.Seller, "selID", "selCompany", product.selID);
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pdtID,selID,pdtName,pdtDate,pdtDescribe,Discontinued,ctgID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ctgID = new SelectList(db.ProductCategory, "pdtCtgID", "fstLayerID", product.ctgID);
            ViewBag.selID = new SelectList(db.Seller, "selID", "selCompany", product.selID);
            return View(product);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
