using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_back_end.Context.Models
{
    public partial class History
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("StationId")]
        public int StationId { get; set; }
        [Required]
        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        public DateTime? Entered { get; set; }
        public DateTime? Left { get; set; }
    }
}
