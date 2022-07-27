using api_back_end.Context;
using api_back_end.Repositories.Histories;
using api_back_end.Repositories.History;
using back_end_api.Repository.Arriving;
using back_end_api.Repository.Departing;
using back_end_api.Repository.FlightRepo;
using back_end_api.Repository.StationRepo;

namespace back_end_api.ControlCenter
{
    public class ControlCenter : IControlCenter
    {
        //Repositories
        public IArrivingFlightsRepository ArrivingFlights { get; private set; }
        public IDepartingFlightsRepository DepartingFlights { get; private set; }
        public IFlightRepository Flights { get; private set; }
        public IStationRepository Stations { get; private set; }
        public IHistoryRepository History { get; private set; }

        //Context
        private readonly FlightsDbContext context;

        //Ctor
        public ControlCenter(FlightsDbContext context)
        {
            this.context = context;
            ArrivingFlights = new ArrivingFlightsRepository(context);
            DepartingFlights = new DepartingFlightsRepository(context);
            Flights = new FlightRepository(context);
            Stations = new StationRepository(context);
            History = new HistoryRepository(context);
        }
        public Task<int> Complete() => context.SaveChangesAsync();
    }
}
