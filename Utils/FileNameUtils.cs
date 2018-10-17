using System.Text.RegularExpressions;

namespace NetCore.Common.Utils
{
    public class FileNameUtils
    {
        public static string UUIDAppendToName(string fileName)
        {
            AssertUtils.AssertNotNull(fileName);

            int dotIdx = fileName.LastIndexOf('.');
            AssertUtils.AssertTrue(dotIdx >= 0, "fileName is invalid.");

            string name = StringUtils.TrimToEmpty(fileName.Substring(0, dotIdx));
            if (name.Length == 0)
            {
                return $"{UUIDUtils.randomUUID()}{fileName.Substring(dotIdx)}";
            }
            return $"{name}-{UUIDUtils.randomUUID()}{fileName.Substring(dotIdx)}";
        }

        public static string UUIDAsName(string fileName)
        {
            AssertUtils.AssertNotNull(fileName);

            int dotIdx = fileName.LastIndexOf('.');
            AssertUtils.AssertTrue(dotIdx >= 0, "fileName is invalid.");

            return $"{UUIDUtils.randomUUID()}{fileName.Substring(dotIdx)}";
        }
    }
}
