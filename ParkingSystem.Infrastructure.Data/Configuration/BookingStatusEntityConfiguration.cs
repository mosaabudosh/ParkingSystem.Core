using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;
using static ParkingSystem.Domain.Enum.Enums;

namespace ParkingSystem.Infrastructure.Data.Configuration
{
    internal class BookingStatusEntityConfiguration : IEntityTypeConfiguration<BookingStatus>
    {
        public void Configure(EntityTypeBuilder<BookingStatus> builder)
        {
            builder.ToTable("BookingStatuses");
            builder.HasKey(e => e.BookingStatusId);

            #region Seeding Data
            builder.HasData(new BookingStatus[]
            {
                new BookingStatus()
                {
                    BookingStatusId = (int)BookingStatuses.Active,
                    Name = "Active",
                },
                new BookingStatus()
                {
                    BookingStatusId = (int)BookingStatuses.Cancelled,
                    Name = "Cancelled",
                },
                new BookingStatus()
                {
                    BookingStatusId = (int)BookingStatuses.Completed,
                    Name = "Completed"
                }
            });
            #endregion
        }
    }
}
