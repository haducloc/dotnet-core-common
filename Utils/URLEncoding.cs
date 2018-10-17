using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Utils
{
    public class URLEncoding
    {
        public static string EncodeParam(string s)
        {
            return System.Net.WebUtility.UrlEncode(s);
        }

        public static string DecodeParam(string s)
        {
            return System.Net.WebUtility.UrlDecode(s);
        }

        public static string EncodePath(string s)
        {
            return Uri.EscapeDataString(s);
        }

        public static string DecodePath(string s)
        {
            return Uri.UnescapeDataString(s);
        }
    }
}
