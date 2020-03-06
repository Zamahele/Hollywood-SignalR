using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HollywoodAPI.Data;
using HollywoodAPI.Model.EventDetail;
using Microsoft.Extensions.Logging;

namespace HollywoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        readonly ILogger<EventDetailsController> _logger;

        public EventDetailsController(ApplicationDbContext context, ILogger<EventDetailsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/EventDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDetail>>> GetEventDetail()
        {
            try
            {
                return await _context.EventDetails.Include(x=>x.Event).Include(c=>c.EventDetailStatus).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest(e.Message);
            }
        }

        // GET: api/EventDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDetail>> GetEventDetail(int id)
        {
            try
            {
                var eventDetail = await _context.EventDetails.FindAsync(id);

                if (eventDetail == null)
                {
                    _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                    return NotFound();
                }

                return eventDetail;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // PUT: api/EventDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEventDetail(int id, EventDetail eventDetail)
        {
            if (id != eventDetail.EventDetailId)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest();
            }

            _context.Entry(eventDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventDetailExists(id))
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

        // POST: api/EventDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<EventDetail>> PostEventDetail(EventDetail eventDetail)
        {
            try
            {
                _context.EventDetails.Add(eventDetail);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetEventDetail", new { id = eventDetail.EventDetailId }, eventDetail);
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest();
            }
        }

        // DELETE: api/EventDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<EventDetail>> DeleteEventDetail(int id)
        {
            var eventDetail = await _context.EventDetails.FindAsync(id);
            if (eventDetail == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            _context.EventDetails.Remove(eventDetail);
            await _context.SaveChangesAsync();

            return eventDetail;
        }

        private bool EventDetailExists(int id)
        {
            return _context.EventDetails.Any(e => e.EventDetailId == id);
        }
    }
}
