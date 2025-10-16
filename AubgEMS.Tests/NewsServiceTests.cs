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
    public class NewsServiceTests
    {
        [Fact]
        public async Task GetAllAsync_Pages_NewestFirst()
        {
            using var t = new TestDb();

            // Remove HasData-seeded posts so the test is deterministic
            t.Db.NewsPosts.RemoveRange(t.Db.NewsPosts);
            await t.Db.SaveChangesAsync();

            t.Db.NewsPosts.AddRange(
                new NewsPost { Title = "Old",    Body = "b", CreatedAt = DateTime.UtcNow.AddDays(-2) },
                new NewsPost { Title = "New",    Body = "b", CreatedAt = DateTime.UtcNow.AddDays(-1) },
                new NewsPost { Title = "Newest", Body = "b", CreatedAt = DateTime.UtcNow }
            );
            await t.Db.SaveChangesAsync();

            var sut = new NewsService(t.Db);
            var page = await sut.GetAllAsync(new PageQuery(1, 2));

            page.TotalCount.Should().Be(3);
            page.Items.Select(i => i.Title).Should().Equal("Newest", "New");
        }
    }
}