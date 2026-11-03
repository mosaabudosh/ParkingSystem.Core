

namespace ParkingSystem.Application.Models
{
    public class ParkingSpotSlotModel
    {
        public int ParkingSpotSlotId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsOccupied { get; set; }
        public int ParkingSpotId { get; set; }
    }
}
