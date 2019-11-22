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
    public class MembersAdmController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: Members
        public ActionResult Index(string searchString)
        {
            var member = from m in db.Member
                          select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                member = member.Where(s => s.mbrName.Contains(searchString));
            }
            return View(member);
        }

        // GET: Members/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        //public ActionResult Create()
        //{
        //    ViewBag.mbrID = new SelectList(db.MemberAccount, "mbrID", "mbrAccount");
        //    return View();
        //}

        //// POST: Members/Create
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "mbrID,mbrName,nickName,mbrPhone,mbrMail,birthDate,Points,mbrAut,signupDate,mbrImage,mbrBlock")] Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Member.Add(member);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.mbrID = new SelectList(db.MemberAccount, "mbrID", "mbrAccount", member.mbrID);
        //    return View(member);
        //}

        // GET: Members/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.mbrID = new SelectList(db.MemberAccount, "mbrID", "mbrAccount", member.mbrID);
            return View(member);
        }

        // POST: Members/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "mbrID,mbrName,nickName,mbrPhone,mbrMail,birthDate,Points,mbrAut,signupDate,mbrImage,mbrBlock")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mbrID = new SelectList(db.MemberAccount, "mbrID", "mbrAccount", member.mbrID);
            return View(member);
        }

        // GET: Members/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Member.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Member member = db.Member.Find(id);
            db.Member.Remove(member);
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
