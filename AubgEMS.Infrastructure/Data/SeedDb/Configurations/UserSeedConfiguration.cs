using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AubgEMS.Infrastructure.Data.SeedDb;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class UserSeedConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var admin = new IdentityUser
            {
                Id = SeedIds.AdminUserId,
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = System.Guid.NewGuid().ToString("D")
            };

            var organizer = new IdentityUser
            {
                Id = SeedIds.OrganizerUserId,
                UserName = "organizer@mail.com",
                NormalizedUserName = "ORGANIZER@MAIL.COM",
                Email = "organizer@mail.com",
                NormalizedEmail = "ORGANIZER@MAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = System.Guid.NewGuid().ToString("D")
            };

            var hasher = new PasswordHasher<IdentityUser>();
            admin.PasswordHash     = hasher.HashPassword(admin, "admin123");
            organizer.PasswordHash = hasher.HashPassword(organizer, "organizer123");

            builder.HasData(admin, organizer);
        }
    }
}