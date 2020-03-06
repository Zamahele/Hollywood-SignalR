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
using BLL.Tournament;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    [AuthenticationAccess(Roles = "Admin")]
    public class EventsController : Controller
    {
        private readonly DataRepository<Event> _context = new DataRepository<Event>();
        private readonly DataRepository<Tournament> _contextTournament = new DataRepository<Tournament>();
        private readonly DataRepository<EventDetail> _contextEventDetail = new DataRepository<EventDetail>();

        // GET: Event
        public ActionResult Index()
        {
            var response = _context.GetAllJsonResult("Events");
            if (!response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Errors");
            }
            var list = response.Content.ReadAsAsync<IEnumerable<Event>>().Result;
            return View(list);

        }


        // GET: Event/Create
        public ActionResult Create()
        {
            ViewBag.TournamentId = new SelectList(_contextTournament.GetAll("Tournaments"), "TournamentId", "TournamentName");
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventId,TournamentId,EventName,EventNumber,EventDateTime,EventEndDateTime,AutoClose")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Save("Events", @event);
                TempData["SuccessfullyNotify"] = "Added successfully";
                return RedirectToAction("Index");
            }

            ViewBag.TournamentId = new SelectList(_contextTournament.GetAll("Tournaments"), "TournamentId", "TournamentName", @event.TournamentId);
            return View(@event);
        }

        // GET: Event/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var @event = _context.GetById("Events", id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.TournamentId = new SelectList(_contextTournament.GetAll("Tournaments"), "TournamentId", "TournamentName", @event.TournamentId);
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventId,TournamentId,EventName,EventNumber,EventDateTime,EventEndDateTime,AutoClose")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Edit("Events", @event, @event.EventId);
                TempData["SuccessfullyNotify"] = "Updated successfully";
                return RedirectToAction("Index");
            }
            ViewBag.TournamentId = new SelectList(_contextTournament.GetAll("Tournaments"), "TournamentId", "TournamentName", @event.TournamentId);
            return View(@event);
        }

        // GET: Event/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewData["eventDetails"] = _contextEventDetail.GetAll("EventDetails").Where(x => x.EventId == id).ToList();
            var @event = _context.GetById("Events", id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var eventDetails = _contextEventDetail.GetAll("EventDetails").Where(x => x.EventId == id).ToList();
            foreach (var detail in eventDetails)
            {
                _context.Delete("EventDetails", detail.EventDetailId);

            }
            _context.Delete("Events", id);
            TempData["SuccessfullyNotify"] = "Deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
