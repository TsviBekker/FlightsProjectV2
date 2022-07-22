using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace back_end_api.Repository.Arriving
{
    public interface IArrivingFlightsRepository : IGenericRepository<ArrivingFlight>
    {
        IEnumerable<ArrivingFlight> GetActiveFlights();
    }
}
