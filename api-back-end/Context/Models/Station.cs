using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api_back_end.Context.Models
{
    public partial class Station
    {
        //public Station()
        //{
        //    ArrivingFlightCurrentStationNavigations = new HashSet<ArrivingFlight>();
        //    ArrivingFlightNextStationNavigations = new HashSet<ArrivingFlight>();
        //    DepartingFlightCurrentStationNavigations = new HashSet<DepartingFlight>();
        //    DepartingFlightNextStationNavigations = new HashSet<DepartingFlight>();
        //}

        [Key]
        public int StationId { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = null!;
        public int PrepTime { get; set; }
        [ForeignKey("FlightId")]
        public int? FlightInStation { get; set; }

        //[ForeignKey("FlightInStation")]
        //[InverseProperty("Stations")]
        //public virtual Flight? FlightInStationNavigation { get; set; }
        //[InverseProperty("CurrentStationNavigation")]
        //public virtual ICollection<ArrivingFlight> ArrivingFlightCurrentStationNavigations { get; set; }
        //[InverseProperty("NextStationNavigation")]
        //public virtual ICollection<ArrivingFlight> ArrivingFlightNextStationNavigations { get; set; }
        //[InverseProperty("CurrentStationNavigation")]
        //public virtual ICollection<DepartingFlight> DepartingFlightCurrentStationNavigations { get; set; }
        //[InverseProperty("NextStationNavigation")]
        //public virtual ICollection<DepartingFlight> DepartingFlightNextStationNavigations { get; set; }
    }
}
