

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;

namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class ParkingSpotSlotEntityConfiguration : IEntityTypeConfiguration<ParkingSpotSlot>
    {
        public void Configure(EntityTypeBuilder<ParkingSpotSlot> builder)
        {
            builder.ToTable("ParkingSpotSlots");
            builder.HasKey(e => e.ParkingSpotSlotId);

            #region Foreignkeys
            builder.HasOne(e => e.ParkingSpot).WithMany(e => e.ParkingSpotSlots).HasForeignKey(f => f.ParkingSpotId);
            #endregion
        }
    }
}
