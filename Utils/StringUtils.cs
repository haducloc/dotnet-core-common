using System.Text.RegularExpressions;

namespace NetCore.Common.Utils
{
    public static class StringUtils
    {
        public static readonly string[] EmptyStrings = { };

        public static string FirstUpperCase(this string str)
        {
            if (string.IsNullOrEmpty(str)) return null;
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        public static string TrimToNull(this string str)
        {
            return TrimToDefault(str, null);
        }

        public static string TrimToEmpty(this string str)
        {
            return TrimToDefault(str, string.Empty);
        }

        public static string TrimToDefault(this string str, string defaultValue)
        {
            if (str == null) return defaultValue;
            str = str.Trim();
            return str.Length == 0 ? defaultValue : str;
        }

        public static string TrimAndUpper(this string str)
        {
            if (str == null) return null;
            str = str.Trim();
            return str.Length == 0 ? null : str.ToUpper();
        }

        public static string TrimAndLower(this string str)
        {
            if (str == null) return null;
            str = str.Trim();
            return str.Length == 0 ? null : str.ToLower();
        }

        static readonly Regex NonDigits = new Regex("[^\\d]+");

        public static string DigitOnly(this string str)
        {
            if (str == null)  return null;
            str = NonDigits.Replace(str, string.Empty);
            return (str.Length > 0) ? str : null;
        }

        public static string Trim(string text, char charToTrim)
        {
            int start = -1;
            while ((++start < text.Length) && (text[start] == charToTrim))
            {
            }
            int end = text.Length;
            while ((--end >= 0) && (text[end] == charToTrim))
            {
            }
            if (start >= end)
            {
                return string.Empty;
            }
            return text.Substring(start, end - start + 1);
        }
    }
}
