using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class NewsPostConfiguration : IEntityTypeConfiguration<NewsPost>
    {
        public void Configure(EntityTypeBuilder<NewsPost> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Body).IsRequired();
            builder.HasIndex(x => x.CreatedAt);

            builder.HasData(
                new NewsPost
                {
                    Id = 1,
                    Title = "Semester Kickoff",
                    Body = "Welcome back! Check out the Welcome Fair and club activities this week.",
                    CreatedAt = new DateTime(2025, 09, 15, 08, 00, 00, DateTimeKind.Utc)
                },
                new NewsPost
                {
                    Id = 2,
                    Title = "Clubs Recruitment Week",
                    Body = "Most clubs are recruiting—visit the Clubs page to learn more and join.",
                    CreatedAt = new DateTime(2025, 09, 18, 09, 30, 00, DateTimeKind.Utc)
                },
                new NewsPost
                {
                    Id = 3,
                    Title = "Pitch Night Applications Open",
                    Body = "Submit your startup idea to join Pitch Night. Shortlisted teams will present on Oct 12. Details on the Events page.",
                    CreatedAt = new DateTime(2025, 09, 20, 14, 00, 00, DateTimeKind.Utc)
                }
            );
        }
    }
}