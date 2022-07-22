using api_back_end.Context;
using api_back_end.Context.Models;
using api_back_end.Services.Utils;

namespace api_back_end.Services.Generator
{
    public class ArrivingGenerator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public ArrivingGenerator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ArrivingGenerator>>();
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

                var newAF = new ArrivingFlight()
                {
                    CurrentStation = context.Stations.OrderBy(s => s.StationId).First().StationId,
                    EnterDate = DateTime.Now,
                    FlightId = flight.FlightId,
                };
                newAF.NextStation = newAF.CurrentStation + 1;

                context.ArrivingFlights.Add(newAF);

                var station = context.Stations.Find(newAF.CurrentStation);
                station!.FlightInStation = newAF.FlightId;
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

                logger.LogInformation("GENERATED ARRIVING FLIGHT");

                await Task.Delay(1000 * 30, stoppingToken);
            }
        }
    }
}
