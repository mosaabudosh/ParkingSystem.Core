using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingSystem.Domain.Domains;
using ParkingSystem.Infrastructure.Data.Configuration;


namespace ParkingSystem.Infrastructure.Data.Context
{
    public class ParkingSystemContext : DbContext
    {
        public IConfiguration Configuration { get; }

        #region Constructor
        public ParkingSystemContext(DbContextOptions<ParkingSystemContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }
        #endregion

        #region Override
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookingEntityConfiguration());
            modelBuilder.ApplyConfiguration(new BookingStatusEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingSpotEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ParkingSpotSlotEntityConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentEntityConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
        }
        #endregion

        #region DbSet
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingStatus> BookingStatuses { get; set; }
        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<ParkingSpotSlot> ParkingSpotSlots { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        #endregion
    }
}
