using api_back_end.Context;
using back_end_api.ControlCenter;

namespace api_back_end.Services.Utils
{
    public class RouteManager
    {
        public int? GetNextStation(int current, bool isArriving, IControlCenter controlCenter)
        {
            if (isArriving)
            {
                if (current < 5) return current + 1;
                if (current == 5)
                {
                    if (controlCenter.Stations.Get(6).Result.FlightInStation == null)
                        return 6;
                    if (controlCenter.Stations.Get(7).Result.FlightInStation == null)
                        return 7;
                }
                return null;
            }

            else
            {
                if (current == 6 || current == 7)
                {
                    return 8;
                }

                if (current == 8)
                {
                    return 4;
                }

                else
                {
                    return null;
                }
            }
        }
    }
}
