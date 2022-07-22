using api_back_end.Context;
using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace back_end_api.Repository.FlightRepo
{
    public class FlightRepository : GenericRepository<Flight>, IFlightRepository
    {
        public FlightRepository(FlightsDbContext context) : base(context)
        {
        }
    }
}
