using api_back_end.Context.Models;
using back_end_api.Repository.Generic;

namespace back_end_api.Repository.StationRepo
{
    public interface IStationRepository : IGenericRepository<Station>
    {
        IEnumerable<Station> GetAvailable();
    }
}
