using api_back_end.Context.Models;

namespace api_back_end.Dtos
{
    public class StationOverviewDto
    {
        public int StationId { get; set; }
        public string? Name { get; set; }
        public Flight? Flight { get; set; }
        public bool IsAvailable { get; set; }
        public int PrepTime { get; set; }
    }
}
