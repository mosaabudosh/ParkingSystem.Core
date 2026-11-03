using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSystem.Domain.Domains;


namespace ParkingSystem.Infrastructure.Data.Configuration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(e => e.UserId);

            #region Seeding
            byte[] passwordHash;
            byte[] passwordSalt;
            using (System.Security.Cryptography.HMACSHA512 HMACSHA512 = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = HMACSHA512.Key;
                passwordHash = HMACSHA512.ComputeHash(System.Text.Encoding.UTF8.GetBytes("123456"));
            }
            Guid userId = new Guid("26435193-38F2-43B2-8841-3AB288E19F9E");
            builder.HasData(new User[] {
                new User()
                {
                      UserId = userId,
                      FullName = "Admin",
                      PasswordHash = passwordHash,
                      PasswordSalt = passwordSalt,
                      Email = "Admin@gmail.com",
                      Phone = "0788647816",
                      CreationDate =  new DateTime(2025,1,1),
                },
            });
            #endregion

            #region Unique
            builder.HasIndex(p => p.Email).IsUnique();
            #endregion
        }
    }
}
