using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AubgEMS.Infrastructure.Data.SeedDb;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class RoleSeedConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = SeedIds.AdminRoleId,     Name = "Admin",     NormalizedName = "ADMIN" },
                new IdentityRole { Id = SeedIds.OrganizerRoleId, Name = "Organizer", NormalizedName = "ORGANIZER" },
                new IdentityRole { Id = SeedIds.StudentRoleId,   Name = "Student",   NormalizedName = "STUDENT" }
            );
        }
    }
}