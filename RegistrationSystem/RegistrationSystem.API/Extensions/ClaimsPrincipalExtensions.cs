using RegistrationSystem.Core.Enums;
using System.Security.Claims;

namespace RegistrationSystem.API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool MatchProvidedId(this ClaimsPrincipal user, string userId)
        {
            var userClaim = user.Claims.SingleOrDefault(u => u.Type.Equals("UserId"));
            if (userClaim == null)
            {
                return false;
            }
            return userClaim.Value == userId;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(nameof(Roles.Admin));
        }
    }
}

