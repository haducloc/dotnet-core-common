using System;

namespace NetCore.Common.Utils
{
    public static class ArrayUtils
    {
        public static byte[] Append(byte[] src1, byte[] src2)
        {
            byte[] arr = new byte[src1.Length + src2.Length];
            Array.Copy(src1, 0, arr, 0, src1.Length);
            Array.Copy(src2, 0, arr, src1.Length, src2.Length);
            return arr;
        }

        public static byte[] Append(byte[] src1, byte[] src2, byte[] src3)
        {
            byte[] arr = new byte[src1.Length + src2.Length + src3.Length];
            Array.Copy(src1, 0, arr, 0, src1.Length);
            Array.Copy(src2, 0, arr, src1.Length, src2.Length);
            Array.Copy(src3, 0, arr, src1.Length + src2.Length, src3.Length);
            return arr;
        }

        public static void Copy(byte[] src, byte[] dest1, byte[] dest2)
        {
            Array.Copy(src, 0, dest1, 0, dest1.Length);
            Array.Copy(src, dest1.Length, dest2, 0, dest2.Length);
        }

        public static void Copy(byte[] src, byte[] dest1, byte[] dest2, byte[] dest3)
        {
            Array.Copy(src, 0, dest1, 0, dest1.Length);
            Array.Copy(src, dest1.Length, dest2, 0, dest2.Length);
            Array.Copy(src, dest1.Length + dest2.Length, dest3, 0, dest3.Length);
        }
    }
}
