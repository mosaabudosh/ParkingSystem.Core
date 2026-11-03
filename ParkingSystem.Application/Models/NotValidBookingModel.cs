

namespace ParkingSystem.Application.Models
{
    public class NotValidBookingModel
    {
        public string BookingId { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }
        public string UserFullName { get; set; }
        public double Minutes { get; set; }
        public bool UserHasFines { get; set; }
    }
}
