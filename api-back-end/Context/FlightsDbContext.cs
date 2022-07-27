using Microsoft.EntityFrameworkCore;
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
    }
}
