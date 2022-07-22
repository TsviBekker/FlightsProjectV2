using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using api_back_end.Context.Models;

namespace api_back_end.Context
{
    public partial class FlightsDbContext : DbContext
    {
        public FlightsDbContext()
        {
        }

        public FlightsDbContext(DbContextOptions<FlightsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ArrivingFlight> ArrivingFlights { get; set; } = null!;
        public virtual DbSet<DepartingFlight> DepartingFlights { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Station> Stations { get; set; } = null!;
        public virtual DbSet<History> Histories { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Flights;Integrated Security=True");
            }
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<ArrivingFlight>(entity =>
        //    {
        //        entity.HasOne(d => d.CurrentStationNavigation)
        //            .WithMany(p => p.ArrivingFlightCurrentStationNavigations)
        //            .HasForeignKey(d => d.CurrentStation)
        //            .HasConstraintName("FK__ArrivingF__Curre__7F2BE32F");

        //        entity.HasOne(d => d.Flight)
        //            .WithMany(p => p.ArrivingFlights)
        //            .HasForeignKey(d => d.FlightId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__ArrivingF__Fligh__7E37BEF6");

        //        entity.HasOne(d => d.NextStationNavigation)
        //            .WithMany(p => p.ArrivingFlightNextStationNavigations)
        //            .HasForeignKey(d => d.NextStation)
        //            .HasConstraintName("FK__ArrivingF__NextS__00200768");
        //    });

        //    modelBuilder.Entity<DepartingFlight>(entity =>
        //    {
        //        entity.HasOne(d => d.CurrentStationNavigation)
        //            .WithMany(p => p.DepartingFlightCurrentStationNavigations)
        //            .HasForeignKey(d => d.CurrentStation)
        //            .HasConstraintName("FK__Departing__Curre__7A672E12");

        //        entity.HasOne(d => d.Flight)
        //            .WithMany(p => p.DepartingFlights)
        //            .HasForeignKey(d => d.FlightId)
        //            .OnDelete(DeleteBehavior.ClientSetNull)
        //            .HasConstraintName("FK__Departing__Fligh__797309D9");

        //        entity.HasOne(d => d.NextStationNavigation)
        //            .WithMany(p => p.DepartingFlightNextStationNavigations)
        //            .HasForeignKey(d => d.NextStation)
        //            .HasConstraintName("FK__Departing__NextS__7B5B524B");
        //    });

        //    modelBuilder.Entity<Flight>(entity =>
        //    {
        //        entity.Property(e => e.FlightId).ValueGeneratedNever();
        //    });

        //    modelBuilder.Entity<Station>(entity =>
        //    {
        //        entity.HasOne(d => d.FlightInStationNavigation)
        //            .WithMany(p => p.Stations)
        //            .HasForeignKey(d => d.FlightInStation)
        //            .HasConstraintName("FK__Stations__Flight__76969D2E");
        //    });

        //    OnModelCreatingPartial(modelBuilder);
        //}

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
