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
    public class ntfRecordsAdmController : Controller
    {
         shopDBEntities db = new shopDBEntities();

       
        public ActionResult Index(string searchtitle)
        {
          

            var ntrcontent = from m in db.ntfRecord
                             select m;
            if(!String.IsNullOrEmpty(searchtitle))
            {
                ntrcontent = ntrcontent.Where(s => s.ntfContent.Contains(searchtitle));
            }

            return View(ntrcontent);
        }



      
        public ActionResult Details(string id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ntfRecord ntfRecord = db.ntfRecord.Find(id);
            if (ntfRecord == null)
            {
                return HttpNotFound();
            }
            return View(ntfRecord);
        }

        // GET: ntfRecordsAdm/Create
        public ActionResult Create()
        {
           
            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName");
            ViewBag.ntfCtgID = new SelectList(db.ntfCategory, "ntfCtgID", "ntfType");
            return View();
        }

        // POST: ntfRecordsAdm/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ntfRecord ntfRecord)
        {
            string GetntfID=db.Database.SqlQuery<string>("Select dbo.GetNtfID()").FirstOrDefault();
            ntfRecord.ntfID = GetntfID;


          

            db.ntfRecord.Add(ntfRecord);
            db.SaveChanges();

            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", ntfRecord.mbrID);
            ViewBag.ntfCtgID = new SelectList(db.ntfCategory, "ntfCtgID", "ntfType", ntfRecord.ntfCtgID);
           
            return RedirectToAction("Index");
        }

        
        public ActionResult Edit(string id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ntfRecord ntfRecord = db.ntfRecord.Find(id);
            if (ntfRecord == null)
            {
                return HttpNotFound();
            }
            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", ntfRecord.mbrID);
            ViewBag.ntfCtgID = new SelectList(db.ntfCategory, "ntfCtgID", "ntfType", ntfRecord.ntfCtgID);
            return View(ntfRecord);
            
        }

        

        // POST: ntfRecordsAdm/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ntfContent,ntfCtgID,mbrID,ntfTitle,ntfTime,ntfID")] ntfRecord ntfRecord)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ntfRecord).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.mbrID = new SelectList(db.Member, "mbrID", "mbrName", ntfRecord.mbrID);
            ViewBag.ntfCtgID = new SelectList(db.ntfCategory, "ntfCtgID", "ntfType", ntfRecord.ntfCtgID);
            return View(ntfRecord);
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult IndexForMember()
        {
            string mbrID = (string)Session["memberID"];
            var ntf = db.ntfRecord.Where(m => m.mbrID == mbrID);

            return View(ntf);
        }

       

       
    }
}
