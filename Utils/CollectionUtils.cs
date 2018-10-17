using NetCore.Common.Base;
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

        public static IList<SelectItem> FirstBlank<T>(this IList<SelectItem> list, T element)
        {
            list.Insert(0, SelectItem.Blank);
            return list;
        }
    }
}
