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
    public class EventsServiceTests
    {
        [Fact]
        public async Task Filters_By_EventType_Club_Department_And_Search()
        {
            using var t = new TestDb();
            var (club1, type1) = await Seed.BasicsAsync(t.Db); // club1 -> dept1, type1

            // make a second department + club
            var dept2 = new Department { Name = $"Dept_{Guid.NewGuid():N}".Substring(0,6) };
            var club2 = new Club { Name = $"Club_{Guid.NewGuid():N}".Substring(0,6), Department = dept2 };
            var type2 = new EventType { Name = $"Type_{Guid.NewGuid():N}".Substring(0,6) };
            t.Db.AddRange(dept2, club2, type2);
            await t.Db.SaveChangesAsync();

            var now = DateTime.UtcNow;

            // four events across types/clubs/departments
            var e1 = new Event { Title="Alpha Workshop", StartTime=now.AddDays(1), Capacity=10, ClubId=club1.Id, EventTypeId=type1.Id, OrganizerId="org" };
            var e2 = new Event { Title="Beta Workshop",  StartTime=now.AddDays(2), Capacity=10, ClubId=club1.Id, EventTypeId=type2.Id, OrganizerId="org" };
            var e3 = new Event { Title="Gamma Seminar",  StartTime=now.AddDays(3), Capacity=10, ClubId=club2.Id, EventTypeId=type1.Id, OrganizerId="org" };
            var e4 = new Event { Title="Alpha Seminar",  StartTime=now.AddDays(4), Capacity=10, ClubId=club2.Id, EventTypeId=type2.Id, OrganizerId="org" };
            t.Db.Events.AddRange(e1, e2, e3, e4);
            await t.Db.SaveChangesAsync();

            var sut = new EventService(t.Db);

            // filter by EventTypeId (type1)
            var qType = new EventQuery(page:1, pageSize:10) { EventTypeId = type1.Id };
            var rType = await sut.GetAllAsync(qType);
            rType.Items.Select(i => i.Id).Should().BeEquivalentTo(new[] { e1.Id, e3.Id });

            // filter by ClubId (club2)
            var qClub = new EventQuery(page:1, pageSize:10) { ClubId = club2.Id };
            var rClub = await sut.GetAllAsync(qClub);
            rClub.Items.Select(i => i.Id).Should().BeEquivalentTo(new[] { e3.Id, e4.Id });

            // filter by DepartmentId (dept2)
            var qDept = new EventQuery(page:1, pageSize:10) { DepartmentId = dept2.Id };
            var rDept = await sut.GetAllAsync(qDept);
            rDept.Items.Select(i => i.Id).Should().BeEquivalentTo(new[] { e3.Id, e4.Id });

            // search by title ("Alpha")
            var qSearch = new EventQuery(page:1, pageSize:10) { Search = "Alpha" };
            var rSearch = await sut.GetAllAsync(qSearch);
            rSearch.Items.Select(i => i.Id).Should().BeEquivalentTo(new[] { e1.Id, e4.Id });
        }
    }
}