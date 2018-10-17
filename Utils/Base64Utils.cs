using System;

namespace NetCore.Common.Utils
{
    public class Base64Utils
    {
        public static string ToBase64(byte[] value)
        {
            return Convert.ToBase64String(value, Base64FormattingOptions.None);
        }

        public static string ToBase64Mime(byte[] value)
        {
            return Convert.ToBase64String(value, Base64FormattingOptions.InsertLineBreaks);
        }

        private static readonly char[] Base64Padding = { '=' };

        public static string ToBase64UrlNP(byte[] value)
        {
            return Convert.ToBase64String(value, Base64FormattingOptions.None)
                    .TrimEnd(Base64Padding)
                    .Replace('+', '-').Replace('/', '_');
        }

        public static byte[] FromBase64(string value)
        {
            return Convert.FromBase64String(value);
        }

        public static byte[] FromBase64Mime(string value)
        {
            return Convert.FromBase64String(value);
        }

        public static byte[] FromBase64UrlNP(string value)
        {
            string v = value.Replace('-', '+').Replace('_', '/');
            switch (value.Length % 4)
            {
                case 2: v += "=="; break;
                case 3: v += "="; break;
            }
            return Convert.FromBase64String(v);
        }
    }
}
