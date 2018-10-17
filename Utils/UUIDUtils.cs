using System;

namespace NetCore.Common.Utils
{
    public class UUIDUtils
    {
        public static string randomUUID()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
