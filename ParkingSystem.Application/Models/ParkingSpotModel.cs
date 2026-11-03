

namespace ParkingSystem.Application.Models
{
    public class ParkingSpotModel
    {
        public int ParkingSpotId { get; set; }
        public string SpotNumber { get; set; }

        public List<ParkingSpotSlotModel> ParkingSpotSlots { get; set; }
    }
}
