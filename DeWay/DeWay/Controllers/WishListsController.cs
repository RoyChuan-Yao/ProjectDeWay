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


        public ActionResult Index()
        {
            string mbrID = (string)Session["memberID"];
            if (mbrID == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var wishes = db.WishList.Where(m => m.mbrID == mbrID).ToList();
            ViewBag.Name = db.Member.Where(m => m.mbrID == mbrID).FirstOrDefault().mbrName;

            return View(wishes);

        }
        [ChildActionOnly]
        public ActionResult _GetWishCard(string pdtID)
        {
            var pdt = db.Product.Find(pdtID);
            return PartialView(pdt);
        }
        // GET: WishLists/Details/5
        public ActionResult Details(string id)
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



        // GET: WishLists/Edit/5
        //public ActionResult Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    WishList wishList = db.WishList.Find(id);
        //    if (wishList == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", wishList.mbrID);
        //    ViewBag.pdtID = new SelectList(db.Product, "pdtID", "selID", wishList.pdtID);
        //    return View(wishList);
        //}

        //// POST: WishLists/Edit/5
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "wishID,pdtID,mbrID")] WishList wishList)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(wishList).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", wishList.mbrID);
        //    ViewBag.pdtID = new SelectList(db.Product, "pdtID", "selID", wishList.pdtID);
        //    return View(wishList);
        //}

        // GET: WishLists/Delete/5
        public ActionResult Delete(string id)
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

        [HttpPost]
        public ActionResult AddToWishList(string pdtID)
        {
            string mbrID = (string)Session["memberID"];
            if (mbrID is null)
            {
                //string js = "(function(){window.location.href =\"../login/login\"})()";
                string js = "";
                Response.StatusCode = 400;
                return JavaScript(js);
            }

            var InWishList = db.WishList.Where(m => m.mbrID == mbrID).Where(m => m.pdtID == pdtID).FirstOrDefault();
            if (InWishList != null)
            {
                db.WishList.Remove(InWishList);
                db.SaveChanges();
                string msg = "商品已經從慾望清單移除！";
                return Content(msg);
            }

            string GetWishID = db.Database.SqlQuery<string>("Select dbo.GetWishID()").FirstOrDefault();

            WishList wish = new WishList();
            wish.pdtID = pdtID;
            wish.wishID = GetWishID;
            wish.mbrID = mbrID;

            db.WishList.Add(wish);
            db.SaveChanges();

            return Content("商品成功新增至慾望清單！");


        }
    }
}