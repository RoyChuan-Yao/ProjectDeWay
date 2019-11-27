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
    public class WishListsController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: WishLists
        public ActionResult WishListIndex(string wishID)
        {
            var cod = db.WishList.Where(a => a.mbrID == wishID).ToList();


            //var cod = db.WishList;
            var m = from p in cod
                    where p.mbrID==wishID
                    select p;
            Session["member"] = wishID;
            return View(m);
            //var wishList = db.WishList.Include(w => w.Member).Include(w => w.Product);
            //return View(wishList.ToList());
           
        }

        // GET: WishLists/Details/5
        public ActionResult WishListDetails(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // GET: WishLists/Create
        public ActionResult WishListCreate()
        {
            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName");
            ViewBag.pdtID = new SelectList(db.Product, "pdtID", "selID");
            return View();
        }

        // POST: WishLists/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WishListCreate([Bind(Include = "wishID,pdtID,mbrID")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.WishList.Add(wishList);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", wishList.mbrID);
            ViewBag.pdtID = new SelectList(db.Product, "pdtID", "selID", wishList.pdtID);
            return View(wishList);
        }

        // GET: WishLists/Edit/5
        public ActionResult WishListEdit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", wishList.mbrID);
            ViewBag.pdtID = new SelectList(db.Product, "pdtID", "selID", wishList.pdtID);
            return View(wishList);
        }

        // POST: WishLists/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WishListEdit([Bind(Include = "wishID,pdtID,mbrID")] WishList wishList)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wishList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", wishList.mbrID);
            ViewBag.pdtID = new SelectList(db.Product, "pdtID", "selID", wishList.pdtID);
            return View(wishList);
        }

        // GET: WishLists/Delete/5
        public ActionResult WishListDelete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WishList wishList = db.WishList.Find(id);
            if (wishList == null)
            {
                return HttpNotFound();
            }
            return View(wishList);
        }

        // POST: WishLists/Delete/5
        [HttpPost, ActionName("Delete")]
      
        public ActionResult DeleteConfirmed(string id)
        {


                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                  
            WishList wishList = db.WishList.Find(id);
            db.WishList.Remove(wishList);
            db.SaveChanges();
            return RedirectToAction("Index");
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
