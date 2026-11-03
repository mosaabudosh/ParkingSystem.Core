

namespace ParkingSystem.Domain.Domains
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }

        public virtual List<UserRole> UserRoles { get; set; }
    }
}
