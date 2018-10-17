namespace NetCore.Common.Utils
{
    public class ValueUtils
    {
        public static long InRange(long checkValue, long min, long max)
        {
            if (checkValue < min)
            {
                return min;
            }
            if (checkValue > max)
            {
                return max;
            }
            return checkValue;
        }

        public static double InRange(double checkValue, double min, double max)
        {
            if (checkValue < min)
            {
                return min;
            }
            if (checkValue > max)
            {
                return max;
            }
            return checkValue;
        }

        public static T ValueOrDefault<T>(T checkValue, T defaultValue)
        {
            T def = default(T);
            if (def == null)
            {
                return (checkValue == null) ? defaultValue : checkValue;
            }
            return def.Equals(checkValue) ? defaultValue : checkValue;
        }
    }
}
