using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using AubgEMS.Controllers;
using AubgEMS.Core.Contracts;
using AubgEMS.Core.Models.Common;
using AubgEMS.Core.Models.Events;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using FluentAssertions;

namespace AubgEMS.Tests
{
    public class MyControllerTests
    {
        [Fact]
        public async Task Events_Returns_Challenge_When_Not_Signed_In()
        {
            var attendance = new Mock<IAttendanceService>();
            var controller = new MyController(attendance.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext() // no user/claims
                }
            };

            var result = await controller.Events(page:1, pageSize:10, ct: CancellationToken.None);

            result.Should().BeOfType<ChallengeResult>();
            attendance.Verify(
                a => a.GetMyEventsAsync(It.IsAny<string>(), It.IsAny<PageQuery>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

        [Fact]
        public async Task Events_Calls_Service_And_Returns_View_For_Signed_In_User()
        {
            var userId = "u-123";
            var attendance = new Mock<IAttendanceService>();
            var expected = new PageResult<EventListItemDto>(new EventListItemDto[0], 0, 1, 10);

            attendance
                .Setup(a => a.GetMyEventsAsync(userId, It.IsAny<PageQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expected);

            var controller = new MyController(attendance.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[]
                        {
                            new Claim(ClaimTypes.NameIdentifier, userId)
                        }, "TestAuth"))
                    }
                }
            };

            var result = await controller.Events(page:1, pageSize:10, ct: CancellationToken.None);

            var view = result.Should().BeOfType<ViewResult>().Subject;
            view.Model.Should().BeSameAs(expected);

            attendance.Verify(
                a => a.GetMyEventsAsync(
                    userId,
                    It.Is<PageQuery>(q => q.Page == 1 && q.PageSize == 10),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}