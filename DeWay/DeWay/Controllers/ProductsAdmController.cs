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
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
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
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
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

       

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
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
