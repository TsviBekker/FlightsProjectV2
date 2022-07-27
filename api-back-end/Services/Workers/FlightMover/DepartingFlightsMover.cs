using api_back_end.Context;
using api_back_end.Context.Models;
using api_back_end.Services.Utils;
using back_end_api.ControlCenter;

namespace api_back_end.Services.Workers.FlightMover
{
    public class DepartingFlightsMover
    {
        private readonly IControlCenter controlCenter;
        private readonly RouteManager routeManager;
        public DepartingFlightsMover()
        {
            controlCenter = new ControlCenter(new FlightsDbContext());
            routeManager = new RouteManager();
        }
        public async Task ReleaseFlight(DepartingFlight df, Station station)
        {
            station.FlightInStation = null;
            station.PrepTime = Randomizer.GeneratePrepTime();
            controlCenter.Stations.Update(station);

            var hist = await controlCenter.History.GetHistoryByFlightAndStation(df.FlightId, station.StationId);
            if (hist != null)
            {
                hist.Left = DateTime.Now;
                controlCenter.History.Update(hist);
            }

            await controlCenter.Complete();
        }

        public async Task RegisterFlight(DepartingFlight df, Station station)
        {
            if (station.FlightInStation != null)
                return;

            station.FlightInStation = df.FlightId;
            controlCenter.Stations.Update(station);

            await controlCenter.History.Add(new History()
            {
                Entered = DateTime.Now,
                FlightId = df.FlightId,
                StationId = station.StationId
            });

            var next = routeManager.GetNextStation(station.StationId, false, controlCenter);
            df.NextStation = next;
            df.CurrentStation = station.StationId;

            controlCenter.DepartingFlights.Update(df);

            await controlCenter.Complete();
        }

        public async Task ForgetFlight(DepartingFlight df)
        {
            df.LeaveDate = DateTime.Now;
            df.CurrentStation = null;
            controlCenter.DepartingFlights.Update(df);
            await controlCenter.Complete();
        }
    }
}
