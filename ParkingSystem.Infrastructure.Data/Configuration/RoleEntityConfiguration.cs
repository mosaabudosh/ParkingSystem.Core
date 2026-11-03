using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;

using static ParkingSystem.Domain.Enum.Enums;

namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(e => e.RoleId);

            #region Seeding Data
            builder.HasData(new Role[]
            {
                new Role()
                {
                    RoleId = (int)Roles.Admin,
                    Name = "Admin" ,
                },
                new Role()
                {
                    RoleId = (int)Roles.Staff,
                    Name = "Staff" ,
                },
                new Role()
                {
                    RoleId = (int)Roles.Customer,
                    Name = "Customer" ,
                },
            });
            #endregion
        }
    }
}
