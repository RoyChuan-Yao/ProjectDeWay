using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DeWay.Models;
using System.IO;

namespace DeWay.Controllers
{
    public class actBulletinsAdmController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: actBulletinsAdm
        public ActionResult Index()
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }

            var actBulletin = db.actBulletin.Include(a => a.Adm);
            return View(actBulletin.ToList());
        }

        // GET: actBulletinsAdm/Details/5
        public ActionResult Details(string id)
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
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
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
            ViewBag.admID = new SelectList(db.Adm, "admID", "admName");
            return View();
        }

        // POST: actBulletinsAdm/Create      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(actBulletin act, HttpPostedFileBase actImage) 
        {
            string fileName = "";
            string GetActID = db.Database.SqlQuery<string>("Select dbo.GetActID()").FirstOrDefault();
            act.actID = GetActID;
            act.actImage = GetActID + ".jpg";
            
            if (actImage != null)
            {
                if (actImage.ContentLength > 0)
                {
                    fileName = GetActID + ".jpg";
                    actImage.SaveAs(Server.MapPath("~/actImage/" + fileName));
                }
            }

            if (actImage == null)
            {
                act.actImage= "act0000000.jpg";
            }
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
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
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
        public ActionResult Edit([Bind(Include = "actID,pdtID,actStrDate,actEndDate,actImage,actDisplay,admID")] actBulletin actBulletin, HttpPostedFileBase actImage)
        {
            string fileName = "";
           
            if (actImage != null)
            {
                if (actImage.ContentLength > 0)
                {
                    fileName = actBulletin.actID + ".jpg";
                    actImage.SaveAs(Server.MapPath("~/actImage/" + fileName));
                    actBulletin.actImage = fileName;
                }
            }

            if (actImage == null)
            {
                actBulletin.actImage = "act0000000.jpg";
            }

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
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
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
