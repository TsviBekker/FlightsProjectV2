using api_back_end.Context;
using api_back_end.Repositories.Histories;
using back_end_api.Repository.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_back_end.Repositories.History
{
    public class HistoryRepository : GenericRepository<Context.Models.History>, IHistoryRepository
    {
        public HistoryRepository(FlightsDbContext context) : base(context)
        {
        }

        public async Task<Context.Models.History?> GetHistoryByFlightAndStation(int flightId, int stationId)
        {
            return await context.Histories.FirstOrDefaultAsync(h=>h.FlightId == flightId && h.StationId == stationId);
        }
    }
}
