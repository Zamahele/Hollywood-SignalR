using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.EventDetailStatus;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    [Authorize]
    public class EventDetailStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventDetailStatus
        public ActionResult Index()
        {
            return View(db.EventDetailStatus.ToList());
        }

        // GET: EventDetailStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetailStatus eventDetailStatus = db.EventDetailStatus.Find(id);
            if (eventDetailStatus == null)
            {
                return HttpNotFound();
            }
            return View(eventDetailStatus);
        }

        // GET: EventDetailStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventDetailStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventDetailStatusId,EventDetailStatusName")] EventDetailStatus eventDetailStatus)
        {
            if (ModelState.IsValid)
            {
                db.EventDetailStatus.Add(eventDetailStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eventDetailStatus);
        }

        // GET: EventDetailStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetailStatus eventDetailStatus = db.EventDetailStatus.Find(id);
            if (eventDetailStatus == null)
            {
                return HttpNotFound();
            }
            return View(eventDetailStatus);
        }

        // POST: EventDetailStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventDetailStatusId,EventDetailStatusName")] EventDetailStatus eventDetailStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventDetailStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eventDetailStatus);
        }

        // GET: EventDetailStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetailStatus eventDetailStatus = db.EventDetailStatus.Find(id);
            if (eventDetailStatus == null)
            {
                return HttpNotFound();
            }
            return View(eventDetailStatus);
        }

        // POST: EventDetailStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventDetailStatus eventDetailStatus = db.EventDetailStatus.Find(id);
            db.EventDetailStatus.Remove(eventDetailStatus);
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
