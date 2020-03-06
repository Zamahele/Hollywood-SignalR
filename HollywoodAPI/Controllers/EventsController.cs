using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HollywoodAPI.Data;
using HollywoodAPI.Model.Event;
using Microsoft.Extensions.Logging;

namespace HollywoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        readonly ILogger<EventsController> _logger;

        public EventsController(ApplicationDbContext context, ILogger<EventsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
        {
            try
            {
                return await _context.Events.Include(x=>x.Tournament).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest(e.Message);
            }
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);

            if (@event == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            return @event;
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
            if (id != @event.EventId)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest();
            }

            _context.Entry(@event).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            try
            {
                _context.Events.Add(@event);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEvent", new { id = @event.EventId }, @event);
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            _context.Events.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
