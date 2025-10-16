using System;
using System.Linq;
using System.Threading.Tasks;
using AubgEMS.Core.Services;
using AubgEMS.Infrastructure.Data.Models;
using FluentAssertions;
using Xunit;

namespace AubgEMS.Tests
{
    public class AnalyticsServiceTests
    {
        [Fact]
        public async Task Kpis_Counts_Work_For_Simple_Setup()
        {
            using var t = new TestDb();
            var (club, type) = await Seed.BasicsAsync(t.Db);
            var now = DateTime.UtcNow;

            // --- Clear any HasData seeded events/attendances so the test is deterministic ---
            // If attendances exist, remove them first to avoid FK conflicts.
            t.Db.EventAttendances.RemoveRange(t.Db.EventAttendances);
            t.Db.Events.RemoveRange(t.Db.Events);
            await t.Db.SaveChangesAsync();
            // -------------------------------------------------------------------------------

            // Arrange exactly two events in the range
            var ev1 = new Event { Title="E1", StartTime=now.AddDays(1), Capacity=2, ClubId=club.Id, EventTypeId=type.Id, OrganizerId="org" };
            var ev2 = new Event { Title="E2", StartTime=now.AddDays(2), Capacity=3, ClubId=club.Id, EventTypeId=type.Id, OrganizerId="org" };
            t.Db.Events.AddRange(ev1, ev2);
            await t.Db.SaveChangesAsync();

            t.Db.EventAttendances.AddRange(
                new EventAttendance { EventId=ev1.Id, UserId="u1" },
                new EventAttendance { EventId=ev1.Id, UserId="u2" },
                new EventAttendance { EventId=ev2.Id, UserId="u1" } // same user twice -> unique attendees = 2
            );
            await t.Db.SaveChangesAsync();

            var sut = new AnalyticsService(t.Db);
            var kpis = await sut.GetKpisAsync(now.AddDays(-1), now.AddDays(7), null, null, null);

            kpis.TotalEvents.Should().Be(2);
            kpis.TotalJoins.Should().Be(3);
            kpis.UniqueAttendees.Should().Be(2);
            kpis.AvgFillRate.Should().BeApproximately(((2.0/2.0) + (1.0/3.0)) / 2.0, 1e-6); // 100% and 33.33% avg
            kpis.UpcomingWeekLoad.Should().Be(2);
        }
    }
}