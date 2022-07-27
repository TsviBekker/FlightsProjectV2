using api_back_end.Context;
using api_back_end.Services.Workers.FlightMover;
using back_end_api.ControlCenter;

namespace api_back_end.Services.Simulator
{
    /// <summary>
    /// This handles arriving flights management (prepping, moving)
    /// </summary>
    public class ArrivingSimulator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        //Ctor
        public ArrivingSimulator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var controlCenter = scope.ServiceProvider.GetRequiredService<IControlCenter>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<ArrivingSimulator>>();

                foreach (var af in await controlCenter.ArrivingFlights.GetActiveFlights())
                {
                    #region why new?
                    // The problem:
                    //  - asnc/await methods take a long time to complete...
                    //  - because they run on the same thread
                    // Solution:
                    //  - using a thread 
                    // The problem:
                    //  - DbContext is not thread safe
                    // The solution:
                    //  - instatntiate a new DbContext in each thread
                    //  - by calling the constructor method of ControlCenter class
                    #endregion

                    controlCenter = new ControlCenter(new FlightsDbContext());

                    var currentStation = await controlCenter.Stations.Get(af.CurrentStation!.Value);

                    //guard close => if flight is waiting for next station to finish
                    if (currentStation!.FlightInStation != null && currentStation.FlightInStation.Value != af.FlightId)
                        continue;

                    //if flight is not yet finished
                    if (currentStation!.PrepTime > 0)
                    {
                        currentStation.PrepTime--;
                        controlCenter.Stations.Update(currentStation);
                        await controlCenter.Complete();
                    }
                    else //SWAP - MOVE Flights
                    {
                        //using a thread so the other processes dont freeze
                        var thread = new Thread(async () =>
                        {
                            var mover = new ArrivingFlightsMover();
                            logger.LogInformation("MOVING FLIGHT");
                            //Steps:
                            // 1. release flight from current station
                            await mover.ReleaseFlight(af, currentStation);

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
                                    // do nothing? it still works because the next iteration when the
                                    //station will be available the flight will enter it
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
