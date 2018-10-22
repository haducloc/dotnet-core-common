using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCore.Common.Base
{
    public class AppVersion
    {
        public int Major { get; set; }

        public int Minor { get; set; }

        public int? Build { get; set; }

        public int? Revision { get; set; }

        static readonly Regex VersionPattern = new Regex(@"^[1-9]\d*(\.[1-9]\d*){1,3}$");

        // Major - increased when the feature set/API of the software changes significantly
        // Minor - increased when notable changes are made, minor API changes or addition of new functionality
        // Build - increased when minor changes are made, typically bug fixes and improvements(though no API changes)
        // Revision - Represents the build instance

        public bool Parse(string version)
        {
            AssertUtils.AssertNotNull(version);
            if (!VersionPattern.IsMatch(version))
            {
                return false;
            }
            string[] numbers = version.Split(new char[] { '.' });

            this.Major = int.Parse(numbers[0]);
            this.Minor = int.Parse(numbers[1]);

            if (numbers.Length >= 3)
            {
                this.Build = int.Parse(numbers[2]);
            }

            if (numbers.Length == 4)
            {
                this.Revision = int.Parse(numbers[3]);
            }
            return true;
        }

        public new string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Major);
            sb.Append('.').Append(Minor);

            if (Build != null)
            {
                sb.Append(".").Append(Build);

                if (Revision != null)
                {
                    sb.Append('.').Append(Revision);
                }
            }
            return sb.ToString();
        }
    }
}
