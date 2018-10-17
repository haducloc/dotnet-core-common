using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Base
{
    public class AppVersion
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int? Revision { get; set; }

        public int? Build { get; set; }

        public bool Parse(string version)
        {
            throw new NotImplementedException();
        }

        public new string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Major);
            sb.Append(".").Append(Minor);

            if (Revision != null)
            {
                sb.Append(".").Append(Revision);

                if (Build != null)
                {
                    sb.Append(".").Append(Build);
                }
            }
            return sb.ToString();
        }
    }
}
