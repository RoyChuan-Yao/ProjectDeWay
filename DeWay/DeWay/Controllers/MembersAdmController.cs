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
