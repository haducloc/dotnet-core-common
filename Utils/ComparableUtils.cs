using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Utils
{
    public class ComparableUtils
    {
        public static int Compare(int c1, int c2)
        {
            if (c1 > c2) return 1;
            if (c1 < c2) return -1;
            return 0;
        }
    }
}
