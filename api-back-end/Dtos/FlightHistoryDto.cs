using api_back_end.Context.Models;

namespace api_back_end.Dtos
{
    public class FlightHistoryDto
    {
        public string? Flight { get; set; }
        public Station? Station { get; set; }
        public DateTime? Entered { get; set; }
        public DateTime? Left { get; set; }
    }
}
