using api_back_end.Context;
using api_back_end.Services.Workers.FlightMover;
using back_end_api.ControlCenter;

namespace api_back_end.Services.Simulator
{
    public class ArrivingSimulator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public ArrivingSimulator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;

        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                //var flightManager = serviceProvider.GetRequiredService<IFlightManager>();
                var controlCenter = scope.ServiceProvider.GetRequiredService<IControlCenter>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ArrivingSimulator>>();
                //var context = new FlightsDbContext();

                foreach (var af in await controlCenter.ArrivingFlights.GetAll())
                {
                    if (af.LeaveDate != null) //if flight left the airport
                        continue;

                    var currentStation = await controlCenter.Stations.Get(af.CurrentStation.Value);

                    controlCenter = new ControlCenter(new FlightsDbContext());

                    //if flight is not yet finished
                    if (currentStation!.PrepTime > 0)
                    {
                        currentStation.PrepTime--;
                        controlCenter.Stations.Update(currentStation);
                        await controlCenter.Complete();
                    }
                    else //SWAP - MOVE Flights
                    {

                        var thread = new Thread(async () =>
                        {
                            var mover = new ArrivingFlightsMover();
                            logger.LogInformation("MOVING FLIGHT");
                            //Steps:
                            // 1. release flight from current station
                            await mover.ReleaseFlight(af, currentStation);
                            // 2. send flight to next station ???

                            //if flight is done forget him
                            if (af.NextStation == null)
                            {
                                await mover.ForgetFlight(af);
                            }
                            //if flight is not done continue on
                            else
                            {
                                controlCenter = new ControlCenter(new FlightsDbContext());
                                var nextStation = await controlCenter.Stations.Get(af.NextStation.Value);
                                //if next station is available
                                if (nextStation!.FlightInStation == null)
                                {
                                    await mover.RegisterFlight(af, nextStation);
                                }
                                //if next station isnt available
                                else
                                {

                                }
                            }

                        });
                        thread.Start();
                    }
                }

                await Task.Delay(1000, stoppingToken);

            }
        }
    }
}
