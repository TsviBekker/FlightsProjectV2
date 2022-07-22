using api_back_end.Context;
using api_back_end.Dtos;
using back_end_api.ControlCenter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_back_end.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IControlCenter controlCenter;
        public OperationsController(IControlCenter controlCenter)
        {
            this.controlCenter = controlCenter;
        }

        [HttpGet("stations-overview")]
        public async Task<ActionResult<IEnumerable<StationOverviewDto>>> GetStations()
        {
            var stations = await controlCenter.Stations.GetAll();

            //.Select = .map method in js
            var ret =
                stations.Select(async s =>
                {
                    return new StationOverviewDto()
                    {
                        //if no flight in station return null else get the flight
                        Flight = s.FlightInStation != null ? await controlCenter.Flights.Get(s.FlightInStation.Value) : null,
                        StationId = s.StationId,
                        Name = s.Name,
                        PrepTime = s.PrepTime,
                    };
                }).Select(t => t.Result);      //since we are getting a task (using async/await) we have to get each result

            return Ok(ret);
        }
        [HttpGet("get-station-history/{stationId:int}")]
        public async Task<ActionResult<IEnumerable<StationHistoryDto>>> GetStations(int stationId)
        {
            var hist = await controlCenter.History.GetAll();
            var ret = hist.Where(h => h.StationId == stationId).Select(h =>
              {
                  return new StationHistoryDto()
                  {
                      Flight = controlCenter.Flights.Get(h.FlightId).Result,
                      Entered = h.Entered,
                      Left = h.Left
                  };
              });
            return Ok(ret);
        }
        [HttpGet("get-af-history")]
        public async Task<ActionResult<IEnumerable<FlightHistoryDto>>> GetArrivingFlightsHistory()
        {
            var hist = await controlCenter.History.GetAll();
            var afs = await controlCenter.ArrivingFlights.GetAll();

            var ret =
                from af in afs
                join h in hist on af.FlightId equals h.FlightId
                select new FlightHistoryDto()
                {
                    Flight = controlCenter.Flights.Get(h.FlightId).Result?.Code,
                    Station = controlCenter.Stations.Get(h.StationId).Result,
                    Entered = h.Entered,
                    Left = h.Left
                };

            return Ok(ret);
        }
        [HttpGet("get-df-history")]
        public async Task<ActionResult<IEnumerable<FlightHistoryDto>>> GetDepartingFlightsHistory()
        {
            var hist = await controlCenter.History.GetAll();
            var dfs = await controlCenter.DepartingFlights.GetAll();

            var ret =
                from df in dfs
                join h in hist on df.FlightId equals h.FlightId
                select new FlightHistoryDto()
                {
                    Flight = controlCenter.Flights.Get(h.FlightId).Result?.Code,
                    Station = controlCenter.Stations.Get(h.StationId).Result,
                    Entered = h.Entered,
                    Left = h.Left
                };

            return Ok(ret);
        }
    }
}
