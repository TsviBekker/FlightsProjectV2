using api_back_end.Context.Models;

namespace api_back_end.Dtos
{
    public class StationHistoryDto
    {
        public Flight? Flight { get; set; }
        public DateTime? Entered { get; set; }
        public DateTime? Left { get; set; }

    }
}
