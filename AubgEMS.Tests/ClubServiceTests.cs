using System;
using System.Linq;
using System.Threading.Tasks;
using AubgEMS.Core.Models.Clubs;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Services;
using AubgEMS.Infrastructure.Data.Models;
using FluentAssertions;
using Xunit;

namespace AubgEMS.Tests
{
    public class ClubServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Filters_By_Department_And_Search()
        {
            using var t = new TestDb();

            var dept = new Department { Name = "Dept_ClubService_Filters" };
            var otherDept = new Department { Name = "Dept_ClubService_Other" };

            var alpha = new Club { Name = "Alpha Club", Department = dept };
            var beta = new Club { Name = "Beta Club", Department = dept };
            var gamma = new Club { Name = "Gamma Club", Department = otherDept };

            t.Db.AddRange(dept, otherDept, alpha, beta, gamma);
            await t.Db.SaveChangesAsync();

            var sut = new ClubService(t.Db);

            var query = new ClubQuery(page: 1, pageSize: 10)
            {
                DepartmentId = dept.Id,
                Search = "Alpha"
            };

            var page = await sut.GetAllAsync(query);

            page.TotalCount.Should().Be(1);
            page.Items.Should().ContainSingle();

            var item = page.Items.Single();
            item.Id.Should().Be(alpha.Id);
            item.Name.Should().Be("Alpha Club");
            item.DepartmentName.Should().Be(dept.Name);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFalse_When_Club_Has_Events()
        {
            using var t = new TestDb();

            var dept = new Department { Name = "Dept_ClubService_Delete" };
            var club = new Club { Name = "Club_With_Events", Department = dept };
            var type = new EventType { Name = "Type_ClubService_Delete" };

            var ev = new Event
            {
                Title = "Event for Club",
                StartTime = DateTime.UtcNow.AddDays(1),
                Capacity = 10,
                Club = club,
                EventType = type,
                OrganizerId = "org"
            };

            t.Db.AddRange(dept, club, type, ev);
            await t.Db.SaveChangesAsync();

            var sut = new ClubService(t.Db);

            var result = await sut.DeleteAsync(club.Id);

            result.Should().BeFalse();
            t.Db.Clubs.Any(c => c.Id == club.Id).Should().BeTrue();
        }
    }
}

