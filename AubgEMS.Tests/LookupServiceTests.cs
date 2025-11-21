using System.Linq;
using System.Threading.Tasks;
using AubgEMS.Core.Services;
using AubgEMS.Infrastructure.Data.Models;
using FluentAssertions;
using Xunit;

namespace AubgEMS.Tests
{
    public class LookupServiceTests
    {
        [Fact]
        public async Task ClubsAsync_Filters_By_Department()
        {
            using var t = new TestDb();

            var dept1 = new Department { Name = "Dept_Lookup_1" };
            var dept2 = new Department { Name = "Dept_Lookup_2" };

            var club1 = new Club { Name = "Club 1", Department = dept1 };
            var club2 = new Club { Name = "Club 2", Department = dept2 };

            t.Db.AddRange(dept1, dept2, club1, club2);
            await t.Db.SaveChangesAsync();

            var sut = new LookupService(t.Db);

            var result = await sut.ClubsAsync(dept1.Id);

            var ids = result.Select(c => c.Id).ToArray();
            ids.Should().Contain(club1.Id);
            ids.Should().NotContain(club2.Id);
        }
    }
}

