using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace back_end_api.Repository.Departing
{
    public interface IDepartingFlightsRepository : IGenericRepository<DepartingFlight>
    {
        Task<IEnumerable<DepartingFlight>> GetActiveFlights();

    }
}
