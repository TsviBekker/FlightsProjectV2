using api_back_end.Context;
using api_back_end.Context.Models;
using api_back_end.Services.Utils;

namespace api_back_end.Services.Generator
{
    /// <summary>
    /// This class generates a new flight instance every (x) seconds
    /// </summary>
    public class DepartingGenerator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private const int ENTERY_STATION_1 = 6;
        private const int ENTERY_STATION_2 = 7;
        private const int NEXT_STATION = 8;
        private const int INTERVAL = 30;

        //Ctor
        public DepartingGenerator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                #region Explanation - Scope
                // The problem:
                //  - Singleton services (background service)
                //  - cannot consume Scoped/Transient services via DI (dep injection)
                // The Solution:
                //  - Create a scope inside the singleton service
                //  - Get required service inside the scope
                //  - Do your work there....
                #endregion

                using var scope = serviceProvider.CreateScope();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DepartingGenerator>>();
                var context = scope.ServiceProvider.GetRequiredService<FlightsDbContext>();

                try
                {
                    //using transaction to roll-back the changes in case something fails 
                    using var transaction = context.Database.BeginTransaction();

                    //making a new flight
                    var flight = new Flight()
                    {
                        FlightId = !context.Flights.Any() ?
                        1 :
                        context.Flights.OrderBy(f => f.FlightId)
                                       .Last().FlightId + 1,
                        Code = Randomizer.GenerateCode()
                    };

                    context.Flights.Add(flight);

                    int next = context.Stations.Find(ENTERY_STATION_1)!.FlightInStation == null ?
                        ENTERY_STATION_1 : ENTERY_STATION_2;

                    //int next = 7;
                    //if (context.Stations.Find(6)!.FlightInStation == null)
                    //{
                    //    next = 6;
                    //}
                    //else if (context.Stations.Find(7)!.FlightInStation == null)
                    //{
                    //    next = 7;
                    //}

                    //linking the flight to a departing flight
                    var newDF = new DepartingFlight()
                    {
                        CurrentStation = next,
                        EnterDate = DateTime.Now,
                        FlightId = flight.FlightId,
                        NextStation = NEXT_STATION
                    };

                    context.DepartingFlights.Add(newDF);

                    //link flight to station
                    var station = context.Stations.Find(newDF.CurrentStation);
                    station!.FlightInStation = newDF.FlightId;
                    context.Stations.Update(station);

                    //make a history
                    var history = new History()
                    {
                        FlightId = flight.FlightId,
                        StationId = station.StationId,
                        Entered = DateTime.Now,
                    };

                    context.Histories.Add(history);

                    context.SaveChanges();

                    transaction.Commit();

                    logger.LogInformation("GENERATED DEPARTING FLIGHT");

                    await Task.Delay(1000 * INTERVAL, stoppingToken);
                }
                //handle exception
                catch (Exception ex)
                {
                    logger.LogCritical(ex.Message);
                }
            }
        }
    }
}
