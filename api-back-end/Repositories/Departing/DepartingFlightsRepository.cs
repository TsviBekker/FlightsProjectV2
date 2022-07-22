using api_back_end.Context;
using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace back_end_api.Repository.Departing
{
    public class DepartingFlightsRepository : GenericRepository<DepartingFlight>, IDepartingFlightsRepository
    {
        public DepartingFlightsRepository(FlightsDbContext context) : base(context)
        {
        }
        public IEnumerable<DepartingFlight> GetActiveFlights()
        {
            return context.DepartingFlights.Where(df => df.LeaveDate == null);
        }
    }
}
