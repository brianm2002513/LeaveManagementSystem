using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveManagementSystem.Web.Data.Configurations
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "f4d35058-b896-4c95-b819-60041a1c3088",
                UserId = "a1b2c3d4-e5f6-7g8h-9i0j-k1l2m3n4o5p6"
            }
        );
        }
    }
}
