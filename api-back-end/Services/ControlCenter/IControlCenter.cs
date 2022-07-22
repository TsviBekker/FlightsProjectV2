using api_back_end.Repositories.Histories;
using back_end_api.Repository.Arriving;
using back_end_api.Repository.Departing;
using back_end_api.Repository.FlightRepo;
using back_end_api.Repository.StationRepo;

namespace back_end_api.ControlCenter
{
    public interface IControlCenter
    { //Unit of work design pattern
        IArrivingFlightsRepository ArrivingFlights { get; }
        IDepartingFlightsRepository DepartingFlights { get; }
        IFlightRepository Flights { get; }
        IStationRepository Stations { get; }
        IHistoryRepository History { get; }
        Task<int> Complete();
    }
}
