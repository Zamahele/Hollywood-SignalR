﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL;
using BLL.Event;
using BLL.Tournament;
using SignalRDbUpdates.Models;

namespace SignalRDbUpdates.Controllers
{
    [Authorize]
    public class TournamentsController : Controller
    {
        private readonly DataRepository<Tournament> _context = new DataRepository<Tournament>();
        private readonly DataRepository<Event> _contextEvent = new DataRepository<Event>();

        // GET: Tournament
        public ActionResult Index()
        {
            return View(_context.GetAll("Tournaments").ToList());
        }

        
        // GET: Tournament/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tournament/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TournamentId,TournamentName")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _context.Save("Tournaments", tournament);
                return RedirectToAction("Index");
            }

            return View(tournament);
        }

        // GET: Tournament/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tournament tournament = _context.GetById("Tournaments", id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // POST: Tournament/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TournamentId,TournamentName")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _context.Edit("Tournaments", tournament, tournament.TournamentId);
                return RedirectToAction("Index");
            }
            return View(tournament);
        }

        // GET: Tournament/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData["LinkedEvents"] = _contextEvent.GetAll("Events").Where(x => x.TournamentId == id).ToList();
            var tournament = _context.GetById("Tournaments", id);
            if (tournament == null)
            {
                return HttpNotFound();
            }
            return View(tournament);
        }

        // POST: Tournament/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var events = _contextEvent.GetAll("Events").Where(x => x.TournamentId == id).ToList();

            foreach (var @event in events)
            {
                _context.Delete("Events", @event.EventId);
            }

            var tournamentId = _context.GetById("Tournaments", id).TournamentId;
            _context.Delete("Tournaments", tournamentId);
            return RedirectToAction("Index");
        }

    }
}