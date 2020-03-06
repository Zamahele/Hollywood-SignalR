using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HollywoodAPI.Data;
using HollywoodAPI.Model.EventDetailStatus;
using Microsoft.Extensions.Logging;

namespace HollywoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailStatusController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        readonly ILogger<EventDetailStatusController> _logger;

        public EventDetailStatusController(ApplicationDbContext context,
            ILogger<EventDetailStatusController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/EventDetailStatus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDetailStatus>>> GetEventDetailStatus()
        {
            try
            {
                return await _context.EventDetailStatus.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest(e.Message);
            }
        }

        // GET: api/EventDetailStatus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetailStatus>> GetEventDetailStatus(int id)
        {
            var eventDetailStatus = await _context.EventDetailStatus.FindAsync(id);

            if (eventDetailStatus == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            return eventDetailStatus;
        }

        // PUT: api/EventDetailStatus/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventDetailStatus(int id, EventDetailStatus eventDetailStatus)
        {
            if (id != eventDetailStatus.EventDetailStatusId)
            {
                return BadRequest();
            }

            _context.Entry(eventDetailStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventDetailStatusExists(id))
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

        // POST: api/EventDetailStatus
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EventDetailStatus>> PostEventDetailStatus(EventDetailStatus eventDetailStatus)
        {
            try
            {
                _context.EventDetailStatus.Add(eventDetailStatus);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEventDetailStatus", new { id = eventDetailStatus.EventDetailStatusId }, eventDetailStatus);
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/EventDetailStatus/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventDetailStatus>> DeleteEventDetailStatus(int id)
        {
            var eventDetailStatus = await _context.EventDetailStatus.FindAsync(id);
            if (eventDetailStatus == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            _context.EventDetailStatus.Remove(eventDetailStatus);
            await _context.SaveChangesAsync();

            return eventDetailStatus;
        }

        private bool EventDetailStatusExists(int id)
        {
            return _context.EventDetailStatus.Any(e => e.EventDetailStatusId == id);
        }
    }
}
