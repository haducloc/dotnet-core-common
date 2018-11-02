using System.Collections.Generic;

namespace NetCore.Common.Base
{
    public static class SelectItemUtils
    {
        public static readonly SelectItem<int?> BlankInt        = new SelectItem<int?>(null, string.Empty);
        public static readonly SelectItem<string> BlankString   = new SelectItem<string>(null, string.Empty);

        public static IList<SelectItem<int?>> FirstBlank(this IList<SelectItem<int?>> list)
        {
            list.Insert(0, BlankInt);
            return list;
        }

        public static IList<SelectItem<string>> FirstBlank(this IList<SelectItem<string>> list)
        {
            list.Insert(0, BlankString);
            return list;
        }
    }
}
