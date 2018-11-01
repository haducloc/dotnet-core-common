using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCore.Common.Crypto
{
    public class RandomUtils
    {
        public static string RandomDigits(int len)
        {
            Random rd = new Random();
            StringBuilder sb = new StringBuilder(len);
            for (int i = 0; i < len; i++)
            {
                sb.Append(rd.Next(0, 10));
            }
            return sb.ToString();
        }

        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static string RandomCode(int length)
        {
            var random = new Random();
            return new string(
                                Enumerable.Repeat(chars, length)
                                .Select(s => s[random.Next(s.Length)]).ToArray()
                             );
        }
    }
}
