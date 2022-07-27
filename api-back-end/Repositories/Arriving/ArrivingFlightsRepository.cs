using api_back_end.Context;
using api_back_end.Context.Models;
using back_end_api.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Arriving
{
    public class ArrivingFlightsRepository : GenericRepository<ArrivingFlight>, IArrivingFlightsRepository
    {
        public ArrivingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ArrivingFlight>> GetActiveFlights()
        {
            return await context.ArrivingFlights.Where(af => af.LeaveDate == null).ToListAsync();
        }
    }
}
