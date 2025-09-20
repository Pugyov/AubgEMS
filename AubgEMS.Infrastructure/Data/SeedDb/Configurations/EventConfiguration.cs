using AubgEMS.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AubgEMS.Infrastructure.Data.SeedDb;

namespace AubgEMS.Infrastructure.Data.SeedDb.Configurations
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(500);

            builder.HasOne(e => e.Club)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.ClubId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.EventType)
                .WithMany(t => t.Events)
                .HasForeignKey(e => e.EventTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Location)
                .WithMany(l => l.Events)
                .HasForeignKey(e => e.LocationId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(e => e.ClubId);
            builder.HasIndex(e => e.EventTypeId);
            builder.HasIndex(e => e.LocationId);

            // Seeding a few events
            builder.HasData(
                new Event
                {
                    Id = 1,
                    Title = "Welcome Fair",
                    Description = "The Welcome Fair is one of the most anticipated events of the semester, bringing together all student clubs and organizations under one roof. Students have the chance to explore the different opportunities AUBG offers beyond academics, from entrepreneurship and debating to sports and arts. The event is designed to showcase the diverse extracurricular activities available and to encourage newcomers to join and become active members of the community. Informational booths, short presentations, and networking opportunities make this fair both informative and exciting, setting the tone for an engaging and dynamic academic year ahead.",
                    StartTime = new DateTime(2025, 10, 01, 16, 00, 00, DateTimeKind.Utc),
                    Capacity = 200,
                    OrganizerId = SeedIds.OrganizerUserId,
                    ClubId = 1,
                    EventTypeId = 4,
                    LocationId = 23,
                    ImageUrl = "https://www.aubg.edu/wp-content/uploads/2024/09/DSC07164-1.jpg"
                },
                new Event
                {
                    Id = 2,
                    Title = "AI 101: Intro Talk",
                    Description = "This introductory session on Artificial Intelligence aims to provide students from all backgrounds with a comprehensive overview of what AI is, how it works, and why it matters. The lecture will cover the basics of machine learning, neural networks, and everyday AI applications, while also addressing the ethical concerns surrounding automation and data usage. It is an open and beginner-friendly talk designed to spark curiosity and inspire further exploration in the field. The event will conclude with an interactive Q&A session where attendees can discuss potential projects, research ideas, and applications of AI in real-world contexts.",
                    StartTime = new DateTime(2025, 10, 05, 18, 30, 00, DateTimeKind.Utc),
                    Capacity = 150,
                    OrganizerId = SeedIds.OrganizerUserId,
                    ClubId = 2,
                    EventTypeId = 3,
                    LocationId = 1,
                    ImageUrl = "https://aubgdaily.com/wp-content/uploads/2017/03/Optimized-_DSC1111.jpg"
                },
                new Event
                {
                    Id = 3,
                    Title = "Pitch Night",
                    Description = "Pitch Night is the ultimate stage for students with entrepreneurial ambitions to present their innovative ideas before a panel of faculty, alumni, and external experts. Each team will have a limited time to pitch their concept, explain the problem it solves, and highlight its market potential. The event is not only a competition but also an opportunity for mentorship and constructive feedback. Even teams that don’t win will walk away with valuable insights to refine their ideas. Pitch Night fosters creativity, teamwork, and problem-solving skills while also connecting aspiring entrepreneurs with potential investors and collaborators within the AUBG network.",
                    StartTime = new DateTime(2025, 10, 12, 19, 00, 00, DateTimeKind.Utc),
                    Capacity = 120,
                    OrganizerId = SeedIds.AdminUserId,
                    ClubId = 1,
                    EventTypeId = 1,
                    LocationId = 14,
                    ImageUrl = "https://www.aubg.edu/wp-content/uploads/2023/05/Elevate-The-Recursive.jpg"
                },
                new Event
                {
                    Id = 4,
                    Title = "Case Study Challenge",
                    Description = "The Case Study Challenge is a team-based competition where participants are given a real-world business scenario and tasked with developing practical, data-driven solutions under time constraints. Teams will analyze the problem, brainstorm strategies, and present their solutions to a panel of judges composed of professors and industry professionals. The challenge pushes students to apply classroom knowledge in a fast-paced and highly collaborative environment. It promotes critical thinking, communication, and decision-making skills, while also exposing participants to real issues faced by modern businesses. Whether winning or not, all teams gain experience that is invaluable for future academic and career pursuits.",
                    StartTime = new DateTime(2025, 10, 20, 17, 00, 00, DateTimeKind.Utc),
                    Capacity = 100,
                    OrganizerId = SeedIds.OrganizerUserId,
                    ClubId = 3,
                
                    EventTypeId = 5,
                    LocationId = 15,
                    ImageUrl = "https://aubgbusinessclub.com/wp-content/uploads/2022/11/Web-59-1-scaled.jpg"
                },
                new Event
                {
                    Id = 5,
                    Title = "MEU Simulation Conference",
                    Description = "The Model European Union (MEU) Simulation Conference is a multi-day event designed to replicate the workings of European institutions. Students will take on the roles of parliament members, ministers, or diplomats and engage in debates, negotiations, and policy-making activities. The simulation provides a unique, immersive experience that combines academic knowledge with practical diplomatic skills. Participants will prepare resolutions, argue positions, and attempt to build consensus in a politically diverse environment. It is both intellectually stimulating and socially engaging, as students interact with peers from different disciplines while exploring complex issues that mirror the challenges faced by policymakers today.",
                    StartTime = new DateTime(2025, 10, 26, 10, 00, 00, DateTimeKind.Utc),
                    Capacity = 180,
                    OrganizerId = SeedIds.AdminUserId,
                    ClubId = 4,
                    EventTypeId = 10,
                    LocationId = 25,
                    ImageUrl = "https://www.aubg.edu/wp-content/uploads/2025/04/AUBG-Today-Hero-Image-3.jpg"
                }
            );
        }
    }
}