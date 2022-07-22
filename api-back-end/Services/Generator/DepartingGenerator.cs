using api_back_end.Context;
using api_back_end.Context.Models;
using api_back_end.Services.Utils;

namespace api_back_end.Services.Generator
{
    public class DepartingGenerator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public DepartingGenerator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DepartingGenerator>>();
                var context = scope.ServiceProvider.GetRequiredService<FlightsDbContext>();

                using var transaction = context.Database.BeginTransaction();

                var flight = new Flight()
                {
                    FlightId = !context.Flights.Any() ?
                    1 :
                    context.Flights.OrderBy(f => f.FlightId)
                                   .Last().FlightId + 1,
                    Code = Randomizer.GenerateCode()
                };

                context.Flights.Add(flight);

                int? next = 7;

                if (context.Stations.Find(6)!.FlightInStation == null)
                {
                    next = 6;
                }
                else if (context.Stations.Find(7)!.FlightInStation == null)
                {
                    next = 7;
                }

                var newDF = new DepartingFlight()
                {
                    CurrentStation = next,
                    EnterDate = DateTime.Now,
                    FlightId = flight.FlightId,
                    NextStation = 8
                };

                context.DepartingFlights.Add(newDF);

                var station = context.Stations.Find(newDF.CurrentStation);
                station.FlightInStation = newDF.FlightId;
                context.Stations.Update(station);

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

                await Task.Delay(1000 * 30, stoppingToken);

                await Task.Delay(1000 * 20, stoppingToken);
            }
        }
    }
}
