

namespace ParkingSystem.Application.Models
{
    public class BookingModel
    {
        public Guid? BookingId { get; set; }
        public int BookingStatusId { get; set; }
        public string UserId { get; set; }
        public int ParkingSpotId { get; set; }
        public PaymentModel Payment { get; set; }
        public List<ParkingSpotSlotModel> ParkingSpotSlots { get; set; }

    }
}
