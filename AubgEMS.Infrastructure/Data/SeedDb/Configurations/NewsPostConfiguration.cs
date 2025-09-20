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
            builder.Property(x => x.ImageUrl).HasMaxLength(500);

            builder.HasData(
                new NewsPost
                {
                    Id = 1,
                    Title = "Semester Kickoff",
                    Body = "The academic year has officially begun, and the campus is buzzing with energy as students return. From classes to social events, there’s something for everyone. The Semester Kickoff marks the start of a new journey filled with opportunities, challenges, and experiences that will shape the year ahead.",
                    CreatedAt = new DateTime(2025, 09, 15, 08, 00, 00, DateTimeKind.Utc),
                    ImageUrl = "https://www.aubg.edu/wp-content/uploads/2022/05/about-hero-bg.jpg"
                },
                new NewsPost
                {
                    Id = 2,
                    Title = "Clubs Recruitment Week",
                    Body = "This week marks the start of club recruitment, where students can sign up for organizations that match their passions and interests. Whether you’re into finance, debating, arts, or volunteering, there’s a place for you. Recruitment Week is the best time to connect with peers and discover your community.",
                    CreatedAt = new DateTime(2025, 09, 18, 09, 30, 00, DateTimeKind.Utc),
                    ImageUrl = "https://iee.bg/wp-content/uploads/2023/08/DSC02107-1-2-scaled.jpg"
                },
                new NewsPost
                {
                    Id = 3,
                    Title = "Pitch Night Applications Open",
                    Body = "Applications are now open for Pitch Night, the signature event where students showcase innovative startup ideas. Teams will compete for recognition and prizes, while gaining valuable feedback from mentors and judges. Don’t miss your chance to present your project and take the first step toward entrepreneurial success.",
                    CreatedAt = new DateTime(2025, 09, 20, 14, 00, 00, DateTimeKind.Utc),
                    ImageUrl = "https://www.aubg.edu/wp-content/uploads/2022/05/students-life-hero.jpg"
                }
            );
        }
    }
}