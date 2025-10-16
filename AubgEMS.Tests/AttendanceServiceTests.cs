using System;
using System.Linq;
using System.Threading.Tasks;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Services;
using AubgEMS.Infrastructure.Data.Models;
using FluentAssertions;
using Xunit;

namespace AubgEMS.Tests
{
    public class AttendanceServiceTests
    {
        [Fact]
        public async Task Join_Then_Leave_Works_Idempotently()
        {
            using var t = new TestDb();
            var (club, type) = await Seed.BasicsAsync(t.Db);

            var ev = new Event
            {
                Title = "Test Event",
                StartTime = DateTime.UtcNow.AddDays(1),
                Capacity = 10,
                ClubId = club.Id,
                EventTypeId = type.Id,
                OrganizerId = "org1"
            };
            t.Db.Events.Add(ev);
            await t.Db.SaveChangesAsync();

            var sut = new AttendanceService(t.Db);

            (await sut.JoinAsync(ev.Id, "u1")).Should().BeTrue();
            (await sut.JoinAsync(ev.Id, "u1")).Should().BeFalse(); // idempotent re-join
            (await sut.LeaveAsync(ev.Id, "u1")).Should().BeTrue();
            (await sut.LeaveAsync(ev.Id, "u1")).Should().BeFalse(); // idempotent re-leave
        }

        [Fact]
        public async Task GetMyEvents_Union_Joined_And_Created_UpcomingFirst()
        {
            using var t = new TestDb();
            var (club, type) = await Seed.BasicsAsync(t.Db);
            var now = DateTime.UtcNow;

            var created = new Event
            {
                Title = "Created",
                StartTime = now.AddDays(2),
                Capacity = 5,
                ClubId = club.Id,
                EventTypeId = type.Id,
                OrganizerId = "me"
            };
            var joined = new Event
            {
                Title = "Joined",
                StartTime = now.AddDays(1),
                Capacity = 5,
                ClubId = club.Id,
                EventTypeId = type.Id,
                OrganizerId = "other"
            };
            t.Db.AddRange(created, joined);
            await t.Db.SaveChangesAsync();

            t.Db.EventAttendances.Add(new EventAttendance { EventId = joined.Id, UserId = "me" });
            await t.Db.SaveChangesAsync();

            var sut = new AttendanceService(t.Db);
            var page = await sut.GetMyEventsAsync("me", new PageQuery(1, 10));

            page.Items.Select(i => i.Title).Should().Equal("Joined", "Created"); // upcoming first
            page.Items.Select(i => i.Id).Should().OnlyHaveUniqueItems();         // union no dupes
        }
    }
}
