

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;

namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class BookingEntityConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");
            builder.HasKey(e => e.BookingId);

            #region Foreignkeys
            builder.HasOne(e => e.BookingStatus).WithMany(e => e.Bookings).HasForeignKey(f => f.BookingStatusId);
            builder.HasOne(e => e.User).WithMany(e => e.Bookings).HasForeignKey(f => f.UserId);
            builder.HasOne(e => e.ParkingSpot).WithMany(e => e.Bookings).HasForeignKey(f => f.ParkingSpotId);
            #endregion
        }
    }
}
