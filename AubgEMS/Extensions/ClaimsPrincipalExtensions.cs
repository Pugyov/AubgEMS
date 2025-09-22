using System.Security.Claims;
using AubgEMS.Core.Constants;

namespace AubgEMS.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string? Id(this ClaimsPrincipal user)
            => user.FindFirstValue(ClaimTypes.NameIdentifier);

        public static bool IsAdmin(this ClaimsPrincipal user)
            => user.IsInRole(RoleNames.Admin);
    }
}