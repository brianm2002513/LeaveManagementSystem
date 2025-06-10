using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Data.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
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
            });
        }
    }
}
