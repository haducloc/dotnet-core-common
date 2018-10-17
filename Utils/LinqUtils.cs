using System;
using System.Collections.Generic;

namespace NetCore.Common.Utils
{
    public static class LinqUtils
    {
        public static void ForEach<E>(this IEnumerable<E> src, Action<E> action)
        {
            foreach (var e in src)
            {
                action(e);
            }
        }
    }
}
