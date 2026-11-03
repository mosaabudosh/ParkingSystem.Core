

namespace ParkingSystem.Domain.Domains
{
    public class Booking
    {
        public Guid BookingId { get; set; }   
        public DateTime CreationDate { get; set; }
        public int BookingStatusId { get; set; }
        public Guid UserId { get; set; }
        public int ParkingSpotId { get; set; }

        public virtual BookingStatus BookingStatus { get; set; }
        public virtual User User { get; set; }
        public virtual ParkingSpot ParkingSpot { get; set; }
        public virtual Payment Payment { get; set; }
    }
}
