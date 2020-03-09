using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HollywoodAPI.Data;
using HollywoodAPI.Model.Tournament;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;


namespace HollywoodAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        readonly ILogger<TournamentsController> _logger;
        private readonly ApplicationDbContext _context;

        public TournamentsController(ApplicationDbContext context,ILogger<TournamentsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Tournaments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tournament>>> GetTournament()
        {
            try
            {
                return await _context.Tournaments.ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest(e.Message);
            }
        }

        // GET: api/Tournaments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tournament>> GetTournament(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);

            if (tournament == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            return tournament;
        }

        // PUT: api/Tournaments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTournament(int id, Tournament tournament)
        {
            if (id != tournament.TournamentId)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()}");
                return BadRequest();
            }

            _context.Entry(tournament).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
                {
                    _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            _logger.LogError($" Error Code: {NoContent().StatusCode.ToString()}");
            return NoContent();
        }

        // POST: api/Tournaments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Tournament>> PostTournament(Tournament tournament)
        {
            try
            {
                await using var connection = (SqlConnection) _context.Database.GetDbConnection();
                var command = connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "Tournament_insert";
                command.Parameters.AddWithValue("@p_TournamentName", tournament.TournamentName);

                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                _logger.LogError($" Error Code: {BadRequest().StatusCode.ToString()} Massage :{e.Message}");
            }

            //_context.Tournaments.Add(tournament);
            //await _context.SaveChangesAsync();
            return CreatedAtAction("GetTournament", new { id = tournament.TournamentId }, tournament);
        }

        // DELETE: api/Tournaments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tournament>> DeleteTournament(int id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament == null)
            {
                _logger.LogError($" Error Code: {NotFound().StatusCode.ToString()}");
                return NotFound();
            }

            _context.Tournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return tournament;
        }

        private bool TournamentExists(int id)
        {
            return _context.Tournaments.Any(e => e.TournamentId == id);
        }
    }
}
