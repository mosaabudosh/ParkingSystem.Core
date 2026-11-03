

namespace ParkingSystem.Domain.Domains
{
    public class ParkingSpot
    {
        public int ParkingSpotId { get; set; }
        public string SpotNumber { get; set; }

        public virtual List<ParkingSpotSlot> ParkingSpotSlots { get; set; }
        public virtual List<Booking> Bookings { get; set; }

    }
}
