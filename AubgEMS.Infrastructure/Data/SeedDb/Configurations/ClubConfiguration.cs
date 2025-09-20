using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AubgEMS.Infrastructure.Data.SeedDb;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class ClubConfiguration : IEntityTypeConfiguration<Club>
    {
        public void Configure(EntityTypeBuilder<Club> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(1000);

            builder.HasOne(c => c.Department)
                .WithMany(d => d.Clubs)
                .HasForeignKey(c => c.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(c => c.DepartmentId);
            builder.HasIndex(c => c.OrganizerId);

            builder.HasData(
                new Club { Id = 1, Name = "The Hub AUBG",                 Description = "Entrepreneurship & innovation hub", OrganizerId = SeedIds.AdminUserId,     DepartmentId = 2 },
                new Club { Id = 2, Name = "Investment Management Club",   Description = "Student-led investing",              OrganizerId = SeedIds.OrganizerUserId, DepartmentId = 3 },
                new Club { Id = 3, Name = "Business Club AUBG",           OrganizerId = SeedIds.OrganizerUserId,             DepartmentId = 1 },
                new Club { Id = 4, Name = "MEU AUBG",                     OrganizerId = SeedIds.AdminUserId,                 DepartmentId = 10 },
                new Club { Id = 5, Name = "Other",                        OrganizerId = SeedIds.AdminUserId } // no department
            );
        }
    }
}