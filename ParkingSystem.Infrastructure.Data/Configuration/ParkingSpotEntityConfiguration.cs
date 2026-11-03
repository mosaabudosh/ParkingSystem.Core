

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;

namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class ParkingSpotEntityConfiguration : IEntityTypeConfiguration<ParkingSpot>
    {
        public void Configure(EntityTypeBuilder<ParkingSpot> builder)
        {
            builder.ToTable("ParkingSpots");
            builder.HasKey(e => e.ParkingSpotId);

         
        }
    }
}
