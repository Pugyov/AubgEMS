using System.Security.Claims;
using AubgEMS.Core.Constants;
using AubgEMS.Extensions;
using FluentAssertions;
using Xunit;

namespace AubgEMS.Tests
{
    public class ClaimsPrincipalExtensionsTests
    {
        [Fact]
        public void Id_And_IsAdmin_Work_For_Admin_User()
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "user-123"),
                new Claim(ClaimTypes.Role, RoleNames.Admin)
            }, "TestAuth");

            var principal = new ClaimsPrincipal(identity);

            principal.Id().Should().Be("user-123");
            principal.IsAdmin().Should().BeTrue();
        }
    }
}

