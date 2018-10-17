using System.Text.RegularExpressions;

namespace NetCore.Common.Utils
{
    public static class StringUtils
    {
        public static readonly string[] EmptyStrings = { };

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
    }
}
