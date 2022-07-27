using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_back_end.Context;
using api_back_end.Context.Models;

namespace api_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartingFlightsController : ControllerBase
    {
        private readonly FlightsDbContext _context;

        public DepartingFlightsController(FlightsDbContext context)
        {
            _context = context;
        }

        // GET: api/DepartingFlights
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DepartingFlight>>> GetDepartingFlights()
        {
          if (_context.DepartingFlights == null)
          {
              return NotFound();
          }
            return await _context.DepartingFlights.ToListAsync();
        }

        // GET: api/DepartingFlights/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DepartingFlight>> GetDepartingFlight(int id)
        {
          if (_context.DepartingFlights == null)
          {
              return NotFound();
          }
            var departingFlight = await _context.DepartingFlights.FindAsync(id);

            if (departingFlight == null)
            {
                return NotFound();
            }

            return departingFlight;
        }

        // PUT: api/DepartingFlights/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepartingFlight(int id, DepartingFlight departingFlight)
        {
            if (id != departingFlight.Id)
            {
                return BadRequest();
            }

            _context.Entry(departingFlight).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartingFlightExists(id))
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

        // POST: api/DepartingFlights
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DepartingFlight>> PostDepartingFlight(DepartingFlight departingFlight)
        {
          if (_context.DepartingFlights == null)
          {
              return Problem("Entity set 'FlightsDbContext.DepartingFlights'  is null.");
          }
            _context.DepartingFlights.Add(departingFlight);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepartingFlight", new { id = departingFlight.Id }, departingFlight);
        }

        // DELETE: api/DepartingFlights/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartingFlight(int id)
        {
            if (_context.DepartingFlights == null)
            {
                return NotFound();
            }
            var departingFlight = await _context.DepartingFlights.FindAsync(id);
            if (departingFlight == null)
            {
                return NotFound();
            }

            _context.DepartingFlights.Remove(departingFlight);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DepartingFlightExists(int id)
        {
            return (_context.DepartingFlights?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
