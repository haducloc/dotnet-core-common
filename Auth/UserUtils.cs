using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace NetCore.Common.Auth
{
    public static class UserUtils
    {
        public const string UserID = "UID";
        public const string UserName = "UName";
        public const string DisplayName = "DName";

        public const string Email = "Email";
        public const string PhoneNumber = "TelNum";

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

        public static void AddRoleClaims(IList<Claim> claims, string roles)
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
            return SplitUtils.Split(roles, ',', ';');
        }

        public static bool ContainRole(string roles, params string[] checkRoles)
        {
            var parsedRoles = ParseRoles(roles);
            if (parsedRoles.Length == 0)
            {
                return false;
            }
            foreach (var checkRole in checkRoles)
            {
                if (parsedRoles.Any(r => r.Equals(checkRole, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidRoles(string roles, Regex validator)
        {
            var parsedRoles = ParseRoles(roles);
            if (parsedRoles.Length == 0)
            {
                return true;
            }
            return !parsedRoles.Any(r => !validator.IsMatch(r));
        }
    }
}