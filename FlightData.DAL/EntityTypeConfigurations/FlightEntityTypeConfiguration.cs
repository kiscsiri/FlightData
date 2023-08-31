using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlightData.DAL.EntityTypeConfigurations
{
    public class FlightEntityTypeConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> builder)
        {
            builder
                .HasOne(f => f.Airline)
                .WithMany()
                .HasForeignKey(f => f.AirlineId);
        }
    }
}
