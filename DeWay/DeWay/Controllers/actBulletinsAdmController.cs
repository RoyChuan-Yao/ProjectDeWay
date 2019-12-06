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
    public class actBulletinsAdmController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: actBulletinsAdm
        public ActionResult Index()
        {
            var actBulletin = db.actBulletin.Include(a => a.Adm);
            return View(actBulletin.ToList());
        }

        // GET: actBulletinsAdm/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actBulletin actBulletin = db.actBulletin.Find(id);
            if (actBulletin == null)
            {
                return HttpNotFound();
            }
            return View(actBulletin);
        }

        // GET: actBulletinsAdm/Create
        public ActionResult Create()
        {
            ViewBag.admID = new SelectList(db.Adm, "admID", "admName");
            return View();
        }

        // POST: actBulletinsAdm/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(actBulletin act) 
        {
            string GetActID = db.Database.SqlQuery<string>("Select dbo.GetActID()").FirstOrDefault();
            act.actID = GetActID;
            //if (ModelState.IsValid)
            //{
            //    db.actBulletin.Add(actBulletin);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            
            db.actBulletin.Add(act);
            db.SaveChanges();
            ViewBag.admID = new SelectList(db.Adm, "admID", "admName", act.admID);
            return View(act);
        }

        // GET: actBulletinsAdm/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actBulletin actBulletin = db.actBulletin.Find(id);
            if (actBulletin == null)
            {
                return HttpNotFound();
            }
            ViewBag.admID = new SelectList(db.Adm, "admID", "admName", actBulletin.admID);
            return View(actBulletin);
        }

        // POST: actBulletinsAdm/Edit/5       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "actID,pdtID,actStrDate,actEndDate,actImage,actDisplay,admID")] actBulletin actBulletin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(actBulletin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.admID = new SelectList(db.Adm, "admID", "admName", actBulletin.admID);
            return View(actBulletin);
        }

        // GET: actBulletinsAdm/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            actBulletin actBulletin = db.actBulletin.Find(id);
            if (actBulletin == null)
            {
                return HttpNotFound();
            }
            return View(actBulletin);
        }

        // POST: actBulletinsAdm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            actBulletin actBulletin = db.actBulletin.Find(id);
            db.actBulletin.Remove(actBulletin);
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
