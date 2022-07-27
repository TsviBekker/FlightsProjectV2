using api_back_end.Context;
using api_back_end.Context.Models;
using api_back_end.Services.Utils;

namespace api_back_end.Services.Generator
{
    /// <summary>
    /// This class generates a new flight instance every (x) seconds
    /// </summary>
    public class ArrivingGenerator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private const int INTERVAL = 45;
        private const int ENTERY_STATION = 1;
        private const int NEXT_STATION = 2;

        //Ctor
        public ArrivingGenerator(IServiceProvider serviceProvider)
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
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ArrivingGenerator>>();
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

                    //linking the flight to an arriving flight
                    var newAF = new ArrivingFlight()
                    {
                        //CurrentStation = context.Stations.OrderBy(s => s.StationId).First().StationId,
                        CurrentStation = ENTERY_STATION,
                        EnterDate = DateTime.Now,
                        FlightId = flight.FlightId,
                    };
                    newAF.NextStation = NEXT_STATION;

                    context.ArrivingFlights.Add(newAF);

                    //link flight to station
                    var station = context.Stations.Find(newAF.CurrentStation); //(1)
                    station!.FlightInStation = newAF.FlightId;
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

                    logger.LogInformation("GENERATED ARRIVING FLIGHT");

                    await Task.Delay(1000 * INTERVAL, stoppingToken);
                }
                //hndle exception
                catch (Exception ex)
                {
                    logger.LogCritical(ex.Message);
                }
            }
        }
    }
}
