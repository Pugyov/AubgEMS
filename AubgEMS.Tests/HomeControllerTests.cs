using System.Threading;
using System.Threading.Tasks;
using AubgEMS.Controllers;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.News;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace AubgEMS.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_Uses_NewsService_And_Returns_Latest_Three_Items()
        {
            var logger = new Mock<ILogger<HomeController>>();
            var news = new Mock<INewsService>();

            var items = new[]
            {
                new NewsListItemDto { Id = 1, Title = "N1" },
                new NewsListItemDto { Id = 2, Title = "N2" },
                new NewsListItemDto { Id = 3, Title = "N3" }
            };

            var page = new PageResult<NewsListItemDto>(items, totalCount: 10, page: 1, pageSize: 3);

            news.Setup(n => n.GetAllAsync(
                    It.Is<PageQuery>(q => q.Page == 1 && q.PageSize == 3),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(page);

            var controller = new HomeController(logger.Object, news.Object);

            var result = await controller.Index(CancellationToken.None);

            var view = result.Should().BeOfType<ViewResult>().Subject;
            view.Model.Should().BeSameAs(items);
        }
    }
}

