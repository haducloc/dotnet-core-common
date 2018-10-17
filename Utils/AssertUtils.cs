using System;
using System.Collections.Generic;

namespace NetCore.Common.Utils
{
    public class AssertUtils
    {
        public static T AssertNotNull<T>(T obj)
        {
            if (obj == null)
                throw new ArgumentNullException(null, "obj must be not null.");
            return obj;
        }

        public static T AssertNotNull<T>(T obj, string message)
        {
            if (obj == null)
                throw new ArgumentNullException(null, message);
            return obj;
        }

        public static T AssertNull<T>(T obj)
        {
            if (obj != null)
                throw new ArgumentException("obj must be null.");
            return obj;
        }

        public static T AssertNull<T>(T obj, string message)
        {
            if (obj != null)
                throw new ArgumentException(message);
            return obj;
        }

        public static string AssertNotEmpty(string str)
        {
            if (string.IsNullOrEmpty(str))
                throw new ArgumentException("str must be not empty.");
            return str;
        }

        public static string AssertNotBlank(string str)
        {
            str = StringUtils.TrimToNull(str);
            if (str == null)
                throw new ArgumentException("str must be not blank.");
            return str;
        }

        public static void AssertTrue(bool expr)
        {
            if (!expr)
                throw new ArgumentException("expr must be true.");
        }

        public static void AssertTrue(bool expr, string message)
        {
            if (!expr)
                throw new ArgumentException(message);
        }

        public static void AssertFalse(bool expr)
        {
            if (expr)
                throw new ArgumentException("expr must be false.");
        }

        public static void AssertFalse(bool expr, string message)
        {
            if (expr)
                throw new ArgumentException(message);
        }

        public static T[] AssertHasElements<T>(T[] array)
        {
            if ((array == null) || (array.Length == 0))
            {
                throw new ArgumentException("array must have elements.");
            }
            return array;
        }

        public static T[] AssertHasElements<T>(T[] array, string message)
        {
            if ((array == null) || (array.Length == 0))
            {
                throw new ArgumentException(message);
            }
            return array;
        }

        public static ICollection<T> AssertHasElements<T>(ICollection<T> collection)
        {
            if ((collection == null) || (collection.Count == 0))
            {
                throw new ArgumentException("collection must have elements.");
            }
            return collection;
        }

        public static ICollection<T> AssertHasElements<T>(ICollection<T> collection, string message)
        {
            if ((collection == null) || (collection.Count == 0))
            {
                throw new ArgumentException(message);
            }
            return collection;
        }
    }
}
