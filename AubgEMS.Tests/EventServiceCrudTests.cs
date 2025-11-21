using System;
using System.Linq;
using System.Threading.Tasks;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;
using AubgEMS.Core.Services;
using AubgEMS.Infrastructure.Data.Models;
using FluentAssertions;
using Xunit;

namespace AubgEMS.Tests
{
    public class EventServiceCrudTests
    {
        [Fact]
        public async Task CreateAsync_Sets_OrganizerId_And_Persists()
        {
            using var t = new TestDb();
            var (club, type) = await Seed.BasicsAsync(t.Db);
            var location = t.Db.Locations.First();

            var model = new EventCreateDto
            {
                Title = "My Event",
                Description = "Desc",
                StartTime = DateTime.UtcNow.AddDays(1),
                Capacity = 20,
                ClubId = club.Id,
                EventTypeId = type.Id,
                LocationId = location.Id,
                ImageUrl = "img"
            };

            var sut = new EventService(t.Db);
            var organizerId = "org-123";

            var result = await sut.CreateAsync(model, organizerId);

            result.Succeeded.Should().BeTrue();
            result.Value.Should().BeGreaterThan(0);

            var ev = await t.Db.Events.FindAsync(result.Value);
            ev.Should().NotBeNull();
            ev!.OrganizerId.Should().Be(organizerId);
            ev.Title.Should().Be("My Event");
            ev.Description.Should().Be("Desc");
        }

        [Fact]
        public async Task UpdateAsync_Fails_For_Different_Organizer()
        {
            using var t = new TestDb();
            var (club, type) = await Seed.BasicsAsync(t.Db);
            var location = t.Db.Locations.First();

            var ev = new Event
            {
                Title = "Original",
                Description = "Original Desc",
                StartTime = DateTime.UtcNow.AddDays(1),
                Capacity = 10,
                ClubId = club.Id,
                EventTypeId = type.Id,
                LocationId = location.Id,
                OrganizerId = "owner-1"
            };

            t.Db.Events.Add(ev);
            await t.Db.SaveChangesAsync();

            var sut = new EventService(t.Db);

            var edit = new EventEditDto
            {
                Id = ev.Id,
                Title = "Updated",
                Description = "Updated Desc",
                StartTime = ev.StartTime.AddDays(1),
                Capacity = 50,
                ClubId = club.Id,
                EventTypeId = type.Id,
                LocationId = location.Id,
                ImageUrl = "img2"
            };

            var result = await sut.UpdateAsync(edit, "different-user");

            result.Succeeded.Should().BeFalse();

            var reloaded = await t.Db.Events.FindAsync(ev.Id);
            reloaded!.Title.Should().Be("Original");
            reloaded.Description.Should().Be("Original Desc");
        }

        [Fact]
        public async Task GetByIdAsync_Projects_Details_With_Lookups()
        {
            using var t = new TestDb();

            var dept = new Department { Name = "Dept_Event_Details" };
            var club = new Club { Name = "Club_Event_Details", Department = dept };
            var type = new EventType { Name = "Type_Event_Details" };
            var location = new Location { Name = "Location_Event_Details", Capacity = 10 };

            var ev = new Event
            {
                Title = "Event Details",
                Description = "Details Desc",
                StartTime = DateTime.UtcNow.AddDays(1),
                Capacity = 30,
                Club = club,
                EventType = type,
                Location = location,
                OrganizerId = "org"
            };

            t.Db.AddRange(dept, club, type, location, ev);
            await t.Db.SaveChangesAsync();

            var sut = new EventService(t.Db);

            var dto = await sut.GetByIdAsync(ev.Id);

            dto.Should().NotBeNull();
            dto!.Id.Should().Be(ev.Id);
            dto.Title.Should().Be("Event Details");
            dto.ClubName.Should().Be("Club_Event_Details");
            dto.EventTypeName.Should().Be("Type_Event_Details");
            dto.LocationName.Should().Be("Location_Event_Details");
        }
    }
}

