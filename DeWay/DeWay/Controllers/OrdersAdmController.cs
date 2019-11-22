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
    public class OrdersAdmController : Controller
    {
        private shopDBEntities db = new shopDBEntities();

        // GET: OrdersAdm
        public ActionResult Index(string searchString)
        {
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

        //// GET: OrdersAdm/Create
        //public ActionResult Create()
        //{
        //    ViewBag.odrStatusID = new SelectList(db.OrderStatus, "odrStatusID", "odrStatus");
        //    ViewBag.pmtID = new SelectList(db.PaymentMethod, "pmtID", "pmtMethod");
        //    return View();
        //}

        //// POST: OrdersAdm/Create
        //// 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        //// 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(Order order)
        //{
        //    string GetorderID = db.Database.SqlQuery<string>("Select dbo.GetOrderID()").FirstOrDefault();
        //    order.odrID = GetorderID;
        //    //if (ModelState.IsValid)
        //    //{
        //    //    db.Order.Add(order);
        //    //    db.SaveChanges();
        //    //    return RedirectToAction("Index");
        //    //}
        //    db.Order.Add(order);
        //    db.SaveChanges();
        //    ViewBag.odrStatusID = new SelectList(db.OrderStatus, "odrStatusID", "odrStatus", order.odrStatusID);
        //    ViewBag.pmtID = new SelectList(db.PaymentMethod, "pmtID", "pmtMethod", order.pmtID);
        //    return View(order);
        //}

        // GET: OrdersAdm/Edit/5
        public ActionResult Edit(string id)
        {
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
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "odrID,pmtID,odrStatusID,recName,recCity,recDist,recAdress,recPhone,pmtDate,odrDate,shpDate,odrNote,traceNumber,cashFlowID")] Order order)
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

        // GET: OrdersAdm/Delete/5
        public ActionResult Delete(string id)
        {
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

        // POST: OrdersAdm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Order order = db.Order.Find(id);
            db.Order.Remove(order);
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
