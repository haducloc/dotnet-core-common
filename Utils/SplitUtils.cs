using System;

namespace NetCore.Common.Utils
{
    public class SplitUtils
    {
        public static string[] Split(string str, char delimiter)
        {
            return str.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
