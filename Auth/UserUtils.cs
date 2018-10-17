using NetCore.Common.Utils;
using System.Collections.Generic;
using System.Security.Claims;

namespace NetCore.Common.Auth
{
    public static class UserUtils
    {
        const string ClaimBase = "/";

        public const string UserID = ClaimBase + "UID";
        public const string UserName = ClaimBase + "UName";
        public const string DisplayName = ClaimBase + "DName";

        public const string Email = ClaimBase + "Email";
        public const string PhoneNumber = ClaimBase + "TelNum";

        public static int GetUserID(this ClaimsPrincipal user)
        {
            var claim = AssertUtils.AssertNotNull(user.FindFirst(UserID));
            return System.Int32.Parse(claim.Value);
        }

        public static string GetUserName(this ClaimsPrincipal user)
        {
            var claim = AssertUtils.AssertNotNull(user.FindFirst(UserName));
            return claim.Value;
        }

        public static string GetDisplayName(this ClaimsPrincipal user)
        {
            var claim = AssertUtils.AssertNotNull(user.FindFirst(DisplayName));
            return claim.Value;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(Email);
            return claim?.Value;
        }

        public static string GetPhoneNumber(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(PhoneNumber);
            return claim?.Value;
        }

        public static string GeRequiredValue(this ClaimsPrincipal user, string claimType)
        {
            Claim claim = AssertUtils.AssertNotNull(user.FindFirst(claimType));
            return AssertUtils.AssertNotNull(claim.Value);
        }

        public static void AddRoleClaims(List<Claim> claims, string roles)
        {
            foreach (var role in UserUtils.ParseRoles(roles))
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToLower()));
            }
        }

        public static string[] ParseRoles(string roles)
        {
            if (roles == null)
            {
                return StringUtils.EmptyStrings;
            }
            string[] userRoles = SplitUtils.Split(roles, ',');
            return userRoles;
        }
    }
}