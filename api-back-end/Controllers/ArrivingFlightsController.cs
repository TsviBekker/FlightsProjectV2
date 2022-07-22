using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_back_end.Context;
using api_back_end.Context.Models;

namespace api_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArrivingFlightsController : ControllerBase
    {
        private readonly FlightsDbContext _context;

        public ArrivingFlightsController(FlightsDbContext context)
        {
            _context = context;
        }

        // GET: api/ArrivingFlights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArrivingFlight>>> GetArrivingFlights()
        {
          if (_context.ArrivingFlights == null)
          {
              return NotFound();
          }
            return await _context.ArrivingFlights.ToListAsync();
        }

        // GET: api/ArrivingFlights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArrivingFlight>> GetArrivingFlight(int id)
        {
          if (_context.ArrivingFlights == null)
          {
              return NotFound();
          }
            var arrivingFlight = await _context.ArrivingFlights.FindAsync(id);

            if (arrivingFlight == null)
            {
                return NotFound();
            }

            return arrivingFlight;
        }

        // PUT: api/ArrivingFlights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArrivingFlight(int id, ArrivingFlight arrivingFlight)
        {
            if (id != arrivingFlight.Id)
            {
                return BadRequest();
            }

            _context.Entry(arrivingFlight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArrivingFlightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ArrivingFlights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ArrivingFlight>> PostArrivingFlight(ArrivingFlight arrivingFlight)
        {
          if (_context.ArrivingFlights == null)
          {
              return Problem("Entity set 'FlightsDbContext.ArrivingFlights'  is null.");
          }
            _context.ArrivingFlights.Add(arrivingFlight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArrivingFlight", new { id = arrivingFlight.Id }, arrivingFlight);
        }

        // DELETE: api/ArrivingFlights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArrivingFlight(int id)
        {
            if (_context.ArrivingFlights == null)
            {
                return NotFound();
            }
            var arrivingFlight = await _context.ArrivingFlights.FindAsync(id);
            if (arrivingFlight == null)
            {
                return NotFound();
            }

            _context.ArrivingFlights.Remove(arrivingFlight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArrivingFlightExists(int id)
        {
            return (_context.ArrivingFlights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
