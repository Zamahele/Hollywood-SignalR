using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Web.UI;
using BLL;
using BLL.Event;
using BLL.EventDetail;
using BLL.EventDetailStatus;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    [Authorize]
    public class EventDetailController : Controller
    {
        private readonly DataRepository<Event> _contextEvent = new DataRepository<Event>();
        private readonly DataRepository<EventDetailStatus> _contextStatus = new DataRepository<EventDetailStatus>();
        private readonly DataRepository<EventDetail> _context = new DataRepository<EventDetail>();

        // GET: EventDetail
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetMessages()
        {
            var messageRepository = new DataRepositoryNotify();
            return PartialView("_Data", messageRepository.GetAllMessages());
        }


        // GET: EventDetails/Create
        public ActionResult Create()
        {
            ViewBag.EventId = new SelectList(_contextEvent.GetAll("Events"), "EventId", "EventName");
            ViewBag.EventDetailStatusId = new SelectList(_contextStatus.GetAll("EventDetailStatus"), "EventDetailStatusId", "EventDetailStatusName");
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
                _context.Save("EventDetails", eventDetail);
                return RedirectToAction("Index");
            }

            ViewBag.EventId = new SelectList(_contextEvent.GetAll("Events"), "EventId", "EventName", eventDetail.EventId);
            ViewBag.EventDetailStatusId = new SelectList(_contextStatus.GetAll("EventDetailStatus"), "EventDetailStatusId", "EventDetailStatusName", eventDetail.EventDetailStatusId);
            return View(eventDetail);
        }

        // GET: EventDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EventDetail eventDetail = _context.GetById("EventDetails", id);
            if (eventDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventId = new SelectList(_contextEvent.GetAll("Events"), "EventId", "EventName", eventDetail.EventId);
            ViewBag.EventDetailStatusId = new SelectList(_contextStatus.GetAll("EventDetailStatus"), "EventDetailStatusId", "EventDetailStatusName", eventDetail.EventDetailStatusId);
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
                _context.Edit("EventDetails", eventDetail, eventDetail.EventDetailId);
                return RedirectToAction("Index");
            }
            ViewBag.EventId = new SelectList(_contextEvent.GetAll("Events"), "EventId", "EventName", eventDetail.EventId);
            ViewBag.EventDetailStatusId = new SelectList(_contextStatus.GetAll("EventDetailStatus"), "EventDetailStatusId", "EventDetailStatusName", eventDetail.EventDetailStatusId);
            return View(eventDetail);
        }

        // GET: EventDetails/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    EventDetail eventDetail = _context.GetById("EventDetails", id);
        //    if (eventDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(eventDetail);
        //}

        //// POST: EventDetails/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var eventDetailId = _context.GetById("EventDetails", id).EventDetailId;
            _context.Delete("EventDetails", eventDetailId);
            return RedirectToAction("Index");
        }
    }
}