using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;
using static ParkingSystem.Domain.Enum.Enums;


namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class UserRoleEntityConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(e => e.UserRoleId);

            #region Foreignkeys
            builder.HasOne(e => e.Role).WithMany(e => e.UserRoles).HasForeignKey(f => f.RoleId);
            builder.HasOne(e => e.User).WithMany(e => e.UserRoles).HasForeignKey(f => f.UserId);
            #endregion

            #region Seeding
            Guid userId = new Guid("26435193-38F2-43B2-8841-3AB288E19F9E");
            builder.HasData(new UserRole[]
            {
                 new UserRole(){
                    UserRoleId = (int)Roles.Admin,
                    UserId = userId ,
                    RoleId = (int)Roles.Admin
                },
                new UserRole(){
                    UserRoleId = (int)Roles.Staff,
                    UserId = userId ,
                    RoleId = (int)Roles.Staff,
                }                
            });
            #endregion
        }
    }
}
