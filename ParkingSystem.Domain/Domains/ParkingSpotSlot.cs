
namespace ParkingSystem.Domain.Domains
{
    public class ParkingSpotSlot
    {
        public int ParkingSpotSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOccupied { get; set; }
        public int ParkingSpotId { get; set; }

        public ParkingSpot ParkingSpot { get; set; }
    }
}
