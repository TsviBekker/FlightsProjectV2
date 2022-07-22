using api_back_end.Context;
using api_back_end.Context.Models;
using api_back_end.Services.Utils;
using back_end_api.ControlCenter;

namespace api_back_end.Services.Workers.FlightMover
{
    public class ArrivingFlightsMover
    {
        private IControlCenter controlCenter;
        private RouteManager routeManager;
        public ArrivingFlightsMover()
        {
            controlCenter = new ControlCenter(new FlightsDbContext());
            routeManager = new RouteManager();
        }
        public async Task ReleaseFlight(ArrivingFlight af, Station station)
        {
            station.FlightInStation = null;
            station.PrepTime = Randomizer.GeneratePrepTime();
            controlCenter.Stations.Update(station);

            var hist = await controlCenter.History.GetHistoryByFlightAndStation(af.FlightId, station.StationId);
            if (hist != null)
            {
                hist.Left = DateTime.Now;
                controlCenter.History.Update(hist);
            }

            await controlCenter.Complete();
        }

        public async Task RegisterFlight(ArrivingFlight af, Station station)
        {
            if (station.FlightInStation != null)
                return;
            station.FlightInStation = af.FlightId;
            controlCenter.Stations.Update(station);

            await controlCenter.History.Add(new History()
            {
                Entered = DateTime.Now,
                FlightId = af.FlightId,
                StationId = station.StationId
            });

            var next = routeManager.GetNextStation(station.StationId, true, controlCenter);
            af.NextStation = next;
            af.CurrentStation = station.StationId;

            controlCenter.ArrivingFlights.Update(af);

            await controlCenter.Complete();
        }

        public async Task ForgetFlight(ArrivingFlight af)
        {
            af.LeaveDate = DateTime.Now;
            af.CurrentStation = null;
            controlCenter.ArrivingFlights.Update(af);
            await controlCenter.Complete();
        }
    }
}
