using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;

namespace NetCore.Common.Crypto
{
    public class PasswordUtils
    {
        static PasswordDigester CreatePasswordDigester()
        {
            return new PasswordDigester { IterationCount = 10_000, KeySize = 32, SaltSize = 32 };
        }

        public static string HashPassword(string password)
        {
            return CreatePasswordDigester().Digest(password);
        }

        public static bool VerifyPassword(string password, string hashed)
        {
            return CreatePasswordDigester().Verify(password, hashed);
        }

        private static readonly string[] PasswordAlphabet = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "abcdefghijkmnopqrstuvwxyz",    // lowercase
                "0123456789",                   // digits
                "$@&#!?*%:+-"                   // symbols
            };

        private static readonly PasswordOptions DefaultPasswordOptions = new PasswordOptions()
        {
            RequiredLength = 8,
            RequiredUniqueChars = 4,
            RequireDigit = true,
            RequireLowercase = true,
            RequireNonAlphanumeric = true,
            RequireUppercase = true
        };

        public static char[] GeneratePassword(PasswordOptions opts = null)
        {
            if (opts == null) opts = DefaultPasswordOptions;

            Random rand = new Random();
            IList<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    PasswordAlphabet[0][rand.Next(0, PasswordAlphabet[0].Length)]);

            if (opts.RequireLowercase)
                chars.Insert(rand.Next(0, chars.Count),
                    PasswordAlphabet[1][rand.Next(0, PasswordAlphabet[1].Length)]);

            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    PasswordAlphabet[2][rand.Next(0, PasswordAlphabet[2].Length)]);

            if (opts.RequireNonAlphanumeric)
                chars.Insert(rand.Next(0, chars.Count),
                    PasswordAlphabet[3][rand.Next(0, PasswordAlphabet[3].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = PasswordAlphabet[rand.Next(0, PasswordAlphabet.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }
            return chars.ToArray();
        }
    }
}