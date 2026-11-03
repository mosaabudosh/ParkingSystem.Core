

namespace ParkingSystem.Domain.Domains
{
    public class Payment
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentTime { get; set; }
        public Guid BookingId { get; set; }

        public virtual Booking Booking { get; set; }
    }
}
