using System.Collections.Generic;

namespace NetCore.Common.Utils
{
    public static class CollectionUtils
    {
        public static IList<T> AddFirst<T>(this IList<T> list, T element)
        {
            list.Insert(0, element);
            return list;
        }
    }
}
