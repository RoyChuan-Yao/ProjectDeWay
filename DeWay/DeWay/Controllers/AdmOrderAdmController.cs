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
    public class AdmOrdersAdmController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: OrdersAdm
        public ActionResult Index(string searchString)
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
            var order = from m in db.Order
                        select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                order = order.Where(s => s.recName.Contains(searchString));
            }
            return View(order);
        }

        // GET: OrdersAdm/Details/5
        public ActionResult Details(string id)
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }




        public ActionResult Edit(string id)
        {
            if (Session["AdmID"] == null)
            { return RedirectToAction("Index", "AdmLogin"); }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Order.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.odrStatusID = new SelectList(db.OrderStatus, "odrStatusID", "odrStatus", order.odrStatusID);
            ViewBag.pmtID = new SelectList(db.PaymentMethod, "pmtID", "pmtMethod", order.pmtID);
            return View(order);
        }

        // POST: OrdersAdm/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include = "odrID,pmtID,odrStatusID,recName,recCity,recDist,recAddress,recPhone,pmtDate,odrDate,shpDate,odrNote,traceNumber,cashFlowID")] Order order)

        {

            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.odrStatusID = new SelectList(db.OrderStatus, "odrStatusID", "odrStatus", order.odrStatusID);
            ViewBag.pmtID = new SelectList(db.PaymentMethod, "pmtID", "pmtMethod", order.pmtID);
            return View(order);
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
