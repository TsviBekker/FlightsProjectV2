using api_back_end.Context;
using api_back_end.Context.Models;
using back_end_api.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace back_end_api.Repository.Departing
{
    public class DepartingFlightsRepository : GenericRepository<DepartingFlight>, IDepartingFlightsRepository
    {
        public DepartingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }
        public async Task<IEnumerable<DepartingFlight>> GetActiveFlights()
        {
            return await context.DepartingFlights.Where(df => df.LeaveDate == null).ToListAsync();
        }
    }
}
