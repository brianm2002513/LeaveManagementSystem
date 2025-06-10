using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.HasData(
            new ApplicationUser
            {
                Id = "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6",
                Email = "admin@localhost",
                NormalizedEmail = "ADMIN@LOCALHOST",
                UserName = "admin@localhost",
                NormalizedUserName = "ADMIN@LOCALHOST",
                PasswordHash = "AQAAAAIAAYagAAAAEPfjW1kzYRml3zk1cKnVyxgArMn4LFqlUwhZnB7hBt7eW9dkeQ0IStYr5by0lc50Kw==", //"Admin@123"
                EmailConfirmed = true,
                AccessFailedCount = 0,
                LockoutEnabled = false,
                LockoutEnd = null,
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                SecurityStamp = "00000000-0000-0000-0000-000000000000",
                ConcurrencyStamp = "11111111-1111-1111-1111-111111111111",
                TwoFactorEnabled = false,
                FirstName = "Default",
                LastName = "Admin",
                DateOfBirth = new DateOnly(1990, 1, 1)
            });
        }
    }
}
