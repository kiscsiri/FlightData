using FlightData.DAL.EntityTypeConfigurations;
using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;

using System.Reflection.Metadata;

namespace FlightData.DAL
{
    public class FlightDataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FlightDataDb");
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Flight> Flights { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CityEntityTypeConfigration).Assembly);
        }
    }
}
