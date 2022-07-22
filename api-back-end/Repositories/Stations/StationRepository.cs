using api_back_end.Context;
using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace back_end_api.Repository.StationRepo
{
    public class StationRepository : GenericRepository<Station>, IStationRepository
    {
        public StationRepository(FlightsDbContext context) : base(context)
        {
        }

        public IEnumerable<Station> GetAvailable()
        {
            return context.Stations.Where(s => s.FlightInStation == null);
        }
    }
}
