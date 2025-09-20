using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AubgEMS.Infrastructure.Data.SeedDb;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class UserRoleSeedConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                // admin@mail.com → Admin
                new IdentityUserRole<string> { UserId = SeedIds.AdminUserId,     RoleId = SeedIds.AdminRoleId },
                // organizer@mail.com → Organizer
                new IdentityUserRole<string> { UserId = SeedIds.OrganizerUserId, RoleId = SeedIds.OrganizerRoleId }
            );
        }
    }
}