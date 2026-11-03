
namespace ParkingSystem.Domain.Domains
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string? AccessToken { get; set; }
        public DateTime? LoginDate { get; set; }
        public DateTime? LogOutDate { get; set; }
        public DateTime CreationDate { get; set; }
        public bool HasFines { get; set; }
        public decimal? TotalFines { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
