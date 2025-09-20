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

            // Seed a few events
            builder.HasData(
                new Event
                {
                    Id = 1,
                    Title = "Welcome Fair",
                    Description = "Clubs showcase and sign-ups.",
                    StartTime = new DateTime(2025, 10, 01, 16, 00, 00, DateTimeKind.Utc),
                    Capacity = 200,
                    OrganizerId = SeedIds.OrganizerUserId,
                    ClubId = 1, // The Hub AUBG
                    EventTypeId = 4, // Club Meeting
                    LocationId = 23 // MB Auditorium
                },
                new Event
                {
                    Id = 2,
                    Title = "AI 101: Intro Talk",
                    Description = "Beginner-friendly overview of AI trends.",
                    StartTime = new DateTime(2025, 10, 05, 18, 30, 00, DateTimeKind.Utc),
                    Capacity = 150,
                    OrganizerId = SeedIds.OrganizerUserId,
                    ClubId = 2, // Investment Management Club
                    EventTypeId = 3, // Lecture
                    LocationId = 1 // BAC Auditorium
                },
                new Event
                {
                    Id = 3,
                    Title = "Pitch Night",
                    Description = "Students pitch startup ideas to a panel.",
                    StartTime = new DateTime(2025, 10, 12, 19, 00, 00, DateTimeKind.Utc),
                    Capacity = 120,
                    OrganizerId = SeedIds.AdminUserId,
                    ClubId = 1, // The Hub AUBG
                    EventTypeId = 1, // Workshop
                    LocationId = 14 // Panitza Library
                },
                // NEW #4
                new Event
                {
                    Id = 4,
                    Title = "Case Study Challenge",
                    Description = "Team-based business case competition hosted by the Business Club.",
                    StartTime = new DateTime(2025, 10, 20, 17, 00, 00, DateTimeKind.Utc),
                    Capacity = 100,
                    OrganizerId = SeedIds.OrganizerUserId,
                    ClubId = 3, // Business Club AUBG
                    EventTypeId = 5, // Challenge
                    LocationId = 15 // Dr. Carl Djerassi Theater Hall
                },
                // NEW #5
                new Event
                {
                    Id = 5,
                    Title = "MEU Simulation Conference",
                    Description = "Model European Union simulation with committees and debates.",
                    StartTime = new DateTime(2025, 10, 26, 10, 00, 00, DateTimeKind.Utc),
                    Capacity = 180,
                    OrganizerId = SeedIds.AdminUserId,
                    ClubId = 4, // MEU AUBG
                    EventTypeId = 10, // Conference
                    LocationId = 25 // Skaptopara II MPR
                }
            );
        }
    }
}