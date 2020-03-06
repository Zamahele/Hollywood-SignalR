using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Event;
using BLL.EventDetail;
using BLL.EventDetailStatus;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    [Authorize]
    public class EventDetailStatusController : Controller
    {
        private readonly DataRepository<EventDetailStatus> _context = new DataRepository<EventDetailStatus>();
        private readonly DataRepository<EventDetail> _contextEventDel = new DataRepository<EventDetail>();

        // GET: EventDetailStatus
        public ActionResult Index()
        {
            return View(_context.GetAll("EventDetailStatus").ToList());
        }

        // GET: EventDetailStatus/Create
        public ActionResult Create()
        {
            ViewBag.EventDetailStatusName = new SelectList(new EventDetailStatus().EventDetailStatusNames());
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
                _context.Save("EventDetailStatus", eventDetailStatus);
                TempData["SuccessfullyNotify"] = "Added successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EventDetailStatusName = new SelectList(new EventDetailStatus().EventDetailStatusNames());
            return View(eventDetailStatus);
        }

        // GET: EventDetailStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Status= new SelectList(new EventDetailStatus().EventDetailStatusNames(), id);
            var eventDetailStatus = _context.GetById("EventDetailStatus", id);
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
                _context.Edit("EventDetailStatus", eventDetailStatus, eventDetailStatus.EventDetailStatusId);
                TempData["SuccessfullyNotify"] = "Updated successfully";
                return RedirectToAction("Index");
            }
            return View(eventDetailStatus);
        }

        //// GET: EventDetailStatus/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var eventDetailStatus = _context.GetById("EventDetailStatus", id);
        //    if (eventDetailStatus == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(eventDetailStatus);
        //}

        // POST: EventDetailStatus/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var eventDetailStatusId = _context.GetById("EventDetailStatus", id).EventDetailStatusId;
            var isLinked = _contextEventDel.GetAll("EventDetails").Where(x => x.EventDetailStatusId == id).ToList();
            if (!isLinked.Any())
            { 
                _context.Delete("EventDetailStatus", eventDetailStatusId);
                TempData["SuccessfullyNotify"] = "Deleted successfully";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Sorry the status is linked";
            return RedirectToAction("Index");
        }

    }
}
