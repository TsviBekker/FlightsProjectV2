using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace api_back_end.Context.Models
{
    public partial class Flight
    {
        //public Flight()
        //{
        //    ArrivingFlights = new HashSet<ArrivingFlight>();
        //    DepartingFlights = new HashSet<DepartingFlight>();
        //    Stations = new HashSet<Station>();
        //}

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FlightId { get; set; }
        [StringLength(8)]
        public string Code { get; set; } = null!;

        //[InverseProperty("Flight")]
        //public virtual ICollection<ArrivingFlight> ArrivingFlights { get; set; }
        //[InverseProperty("Flight")]
        //public virtual ICollection<DepartingFlight> DepartingFlights { get; set; }
        //[InverseProperty("FlightInStationNavigation")]
        //public virtual ICollection<Station> Stations { get; set; }
    }
}
