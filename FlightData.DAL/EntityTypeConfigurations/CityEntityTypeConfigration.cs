using FlightData.Model.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightData.DAL.EntityTypeConfigurations
{
    public class CityEntityTypeConfigration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasMany(c => c.ArrivalFlights)
                .WithOne(f => f.DestinationCity)
                .HasForeignKey(f => f.DestinationCityId);

            builder.HasMany(c => c.DepartureFlights)
                .WithOne(f => f.StartCity)
                .HasForeignKey(f => f.StartCityId);
        }
    }
}
