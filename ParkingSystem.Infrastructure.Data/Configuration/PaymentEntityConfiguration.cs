

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;

namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class PaymentEntityConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(e => e.PaymentId);

            #region Field Attribute 
            builder.Property(p => p.Amount).HasColumnType("decimal(18, 4)");
            #endregion

            #region Foreignkeys
            builder.HasOne(e => e.Booking).WithOne(e => e.Payment).HasForeignKey<Payment>(f => f.BookingId);
            #endregion
        }
    }
}
