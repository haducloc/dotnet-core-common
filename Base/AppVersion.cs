using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace NetCore.Common.Base
{
    public class AppVersion : IComparable<AppVersion>
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

        public static AppVersion Parse(string version)
        {
            AssertUtils.AssertNotNull(version);
            if (!VersionPattern.IsMatch(version))
            {
                return null;
            }
            string[] numbers = version.Split(new char[] { '.' });

            var appVersion = new AppVersion
            {
                Major = int.Parse(numbers[0]),
                Minor = int.Parse(numbers[1])
            };

            if (numbers.Length >= 3)
            {
                appVersion.Build = int.Parse(numbers[2]);
            }

            if (numbers.Length == 4)
            {
                appVersion.Revision = int.Parse(numbers[3]);
            }
            return appVersion;
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

        public int CompareTo(AppVersion other)
        {
            AssertUtils.AssertNotNull(other);

            var compare = ComparableUtils.Compare(this.Major, other.Major);
            if (compare != 0) return compare;

            compare = ComparableUtils.Compare(this.Minor, other.Minor);
            if (compare != 0) return compare;

            compare = ComparableUtils.Compare(this.Build ?? int.MinValue, other.Build ?? int.MinValue);
            if (compare != 0) return compare;

            return ComparableUtils.Compare(this.Revision ?? int.MinValue, other.Revision ?? int.MinValue);
        }
    }
}
