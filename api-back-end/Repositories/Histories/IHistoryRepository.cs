using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace api_back_end.Repositories.Histories
{
    public interface IHistoryRepository : IGenericRepository<Context.Models.History>
    {
        Task<Context.Models.History?> GetHistoryByFlightAndStation(int flightId, int stationId);
    }
}
