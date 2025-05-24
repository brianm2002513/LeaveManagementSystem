using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeaveManagementSystem.Web.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Add any customizations after calling base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole
            {
                Id = "8ad40b76-850e-4e7b-8103-52560e603ce4",
                Name = "Employee",
                NormalizedName = "EMPLOYEE",
            },
            new IdentityRole
            {
                Id = "c70b767d-e9fc-45fb-b1b0-bd82fad02283",
                Name = "Supervisor",
                NormalizedName = "SUPERVISOR",
            },
            new IdentityRole
            {
                Id = "f4d35058-b896-4c95-b819-60041a1c3088",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
            }
            );
        builder.Entity<ApplicationUser>().HasData(
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
            }
            );

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "f4d35058-b896-4c95-b819-60041a1c3088",
                UserId = "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6"
            }
        );
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<Period> Periods { get; set; }
}
