using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Utils
{
    public static class DateUtils
    {
        public const string Iso8601Date = "yyyy-MM-dd";

        public const string CstZoneId = "Central Standard Time";

        /// <summary>
        /// java.lang.System.currentTimeMillis
        /// </summary>
        public static long CurrentTimeMillis => DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        public static DateTimeOffset CstNow => TimeZoneInfo.ConvertTime(DateTimeOffset.Now, TimeZoneInfo.FindSystemTimeZoneById(CstZoneId));

        public static DateTimeOffset CstNowNoMillis => TruncToSecond(CstNow);

        public static DateTime NowNoMillis => TruncToSecond(DateTime.Now);

        public static DateTime UtcNowNoMillis => TruncToSecond(DateTime.UtcNow);

        public static DateTime TruncToSecond(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
        }

        public static DateTime TruncToMinute(this DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0, dt.Kind);
        }

        public static DateTimeOffset TruncToSecond(this DateTimeOffset dto)
        {
            return new DateTimeOffset(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, dto.Second, 0, dto.Offset);
        }

        public static DateTimeOffset TruncToMinute(this DateTimeOffset dto)
        {
            return new DateTimeOffset(dto.Year, dto.Month, dto.Day, dto.Hour, dto.Minute, 0, 0, dto.Offset);
        }

        public static bool IsFutureTime(long timeMillis, int leewayMs)
        {
            AssertUtils.AssertTrue(leewayMs >= 0);
            return CurrentTimeMillis - leewayMs < timeMillis;
        }

        public static bool IsPastTime(long timeMillis, int leewayMs)
        {
            AssertUtils.AssertTrue(leewayMs >= 0);
            return CurrentTimeMillis + leewayMs >= timeMillis;
        }
    }
}
