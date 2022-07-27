using api_back_end.Context;
using api_back_end.Services.Workers.FlightMover;
using back_end_api.ControlCenter;

namespace api_back_end.Services.Simulator
{
    /// <summary>
    /// This handles departing flights management (prepping, moving)
    /// </summary>
    public class DepartingSimulator : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        //Ctor
        public DepartingSimulator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = serviceProvider.CreateScope();
                var controlCenter = scope.ServiceProvider.GetRequiredService<IControlCenter>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<DepartingSimulator>>();

                //Active flights = flights who are still in the airport
                foreach (var df in await controlCenter.DepartingFlights.GetActiveFlights())
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

                    var currentStation = await controlCenter.Stations.Get(df.CurrentStation!.Value);

                    //guard close => if flight is waiting for next station to finish
                    if (currentStation!.FlightInStation != null && currentStation.FlightInStation.Value != df.FlightId)
                        continue;

                    //if flight is not yet finished
                    if (currentStation!.PrepTime > 0)
                    {
                        currentStation.PrepTime--;
                        controlCenter.Stations.Update(currentStation);
                        await controlCenter.Complete();
                    }
                    else //SWAP - Move flights
                    {
                        //using a thread so the other processes dont freeze
                        var thread = new Thread(async () =>
                        {
                            var mover = new DepartingFlightsMover();
                            logger.LogInformation("MOVING FLIGHT");
                            //Steps:
                            // 1. release flight from current station
                            await mover.ReleaseFlight(df, currentStation);

                            //if flight is done forget him
                            if (df.NextStation == null)
                            {
                                await mover.ForgetFlight(df);
                            }
                            //move flight to next
                            else
                            {
                                controlCenter = new ControlCenter(new FlightsDbContext());

                                var nextStation = await controlCenter.Stations.Get(df.NextStation.Value);
                                //if next station is available
                                if (nextStation!.FlightInStation == null)
                                {
                                    await mover.RegisterFlight(df, nextStation);
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
