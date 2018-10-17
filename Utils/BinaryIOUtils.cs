using System.IO;

namespace NetCore.Common.Utils
{
    public static class BinaryIOUtils
    {
        public static void WriteOpt(this BinaryWriter w, bool? value)
        {
            w.Write(value != null ? (byte)1 : (byte)0);
            if (value != null)
            {
                w.Write(value.Value);
            }
        }

        public static bool? ReadBoolOpt(this BinaryReader r)
        {
            byte b = r.ReadByte();
            if (b == 0)
            {
                return null;
            }
            return r.ReadBoolean();
        }

        public static void WriteOpt(this BinaryWriter w, int? value)
        {
            w.Write(value != null ? (byte)1 : (byte)0);
            if (value != null)
            {
                w.Write(value.Value);
            }
        }

        public static int? ReadIntOpt(this BinaryReader r)
        {
            byte b = r.ReadByte();
            if (b == 0)
            {
                return null;
            }
            return r.ReadInt32();
        }

        public static void WriteOpt(this BinaryWriter w, long? value)
        {
            w.Write(value != null ? (byte)1 : (byte)0);
            if (value != null)
            {
                w.Write(value.Value);
            }
        }

        public static long? ReadLongOpt(this BinaryReader r)
        {
            byte b = r.ReadByte();
            if (b == 0)
            {
                return null;
            }
            return r.ReadInt64();
        }

        public static void WriteOpt(this BinaryWriter w, double? value)
        {
            w.Write(value != null ? (byte)1 : (byte)0);
            if (value != null)
            {
                w.Write(value.Value);
            }
        }

        public static double? ReadDoubleOpt(this BinaryReader r)
        {
            byte b = r.ReadByte();
            if (b == 0)
            {
                return null;
            }
            return r.ReadDouble();
        }

        public static void WriteOpt(this BinaryWriter w, string value)
        {
            w.Write(value != null ? (byte)1 : (byte)0);
            if (value != null)
            {
                w.Write(value);
            }
        }

        public static string ReadStringOpt(this BinaryReader r)
        {
            byte b = r.ReadByte();
            if (b == 0)
            {
                return null;
            }
            return r.ReadString();
        }
    }
}
