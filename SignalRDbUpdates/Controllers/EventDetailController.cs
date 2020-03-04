using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using BLL.EventDetail;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    [Authorize]
    public class EventDetailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventDetail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessages()
        {
            var messageRepository = new DataRepository();
             return PartialView("_Data", messageRepository.GetAllMessages());
            //return PartialView("_MessagesList", messageRepository.GetAllMessages());
        }


        // GET: EventDetails/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(db.Event, "EventId", "EventName");
            ViewBag.EventDetailStatusId = new SelectList(db.EventDetailStatus, "EventDetailStatusId", "EventDetailStatusName");
            return View();
        }

        // POST: EventDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventDetailId,EventId,EventDetailStatusId,EventDetailName,EventDetailNumber,EventDetailOdd,FinishingPosition,FirstTimer")] EventDetail eventDetail)
        {
            if (ModelState.IsValid)
            {
                db.EventDetails.Add(eventDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(db.Event, "EventId", "EventName", eventDetail.EventId);
            ViewBag.EventDetailStatusId = new SelectList(db.EventDetailStatus, "EventDetailStatusId", "EventDetailStatusName", eventDetail.EventDetailStatusId);
            return View(eventDetail);
        }

        // GET: EventDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(db.Event, "EventId", "EventName", eventDetail.EventId);
            ViewBag.EventDetailStatusId = new SelectList(db.EventDetailStatus, "EventDetailStatusId", "EventDetailStatusName", eventDetail.EventDetailStatusId);
            return View(eventDetail);
        }

        // POST: EventDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventDetailId,EventId,EventDetailStatusId,EventDetailName,EventDetailNumber,EventDetailOdd,FinishingPosition,FirstTimer")] EventDetail eventDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eventDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(db.Event, "EventId", "EventName", eventDetail.EventId);
            ViewBag.EventDetailStatusId = new SelectList(db.EventDetailStatus, "EventDetailStatusId", "EventDetailStatusName", eventDetail.EventDetailStatusId);
            return View(eventDetail);
        }

        // GET: EventDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetail eventDetail = db.EventDetails.Find(id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            return View(eventDetail);
        }

        // POST: EventDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventDetail eventDetail = db.EventDetails.Find(id);
            db.EventDetails.Remove(eventDetail);
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