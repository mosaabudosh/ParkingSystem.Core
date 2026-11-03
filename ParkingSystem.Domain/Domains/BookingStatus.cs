

namespace ParkingSystem.Domain.Domains
{
    public class BookingStatus
    {
        public int BookingStatusId { get; set; }
        public string Name { get; set; }

        public virtual List<Booking> Bookings { get; set; }
    }
}
