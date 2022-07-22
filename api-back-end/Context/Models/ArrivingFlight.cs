using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api_back_end.Context.Models
{
    public partial class ArrivingFlight
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EnterDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? LeaveDate { get; set; }
        [ForeignKey("FlightId")]
        public int FlightId { get; set; }
        [ForeignKey("StationId")]
        public int? CurrentStation { get; set; }
        [ForeignKey("StationId")]
        public int? NextStation { get; set; }

        //[ForeignKey("CurrentStation")]
        //[InverseProperty("ArrivingFlightCurrentStationNavigations")]
        //public virtual Station? CurrentStationNavigation { get; set; }
        //[ForeignKey("FlightId")]
        //[InverseProperty("ArrivingFlights")]
        //public virtual Flight Flight { get; set; } = null!;
        //[ForeignKey("NextStation")]
        //[InverseProperty("ArrivingFlightNextStationNavigations")]
        //public virtual Station? NextStationNavigation { get; set; }
    }
}
