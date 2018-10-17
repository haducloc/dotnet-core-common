using NetCore.Common.Utils;
using System;

namespace NetCore.Common.Base
{
    public class BaseEncoder
    {
        public static readonly BaseEncoder Base64 = new BaseEncoder("Base64");
        public static readonly BaseEncoder Base64UrlNP = new BaseEncoder("Base64UrlNP");
        public static readonly BaseEncoder Base64Mime = new BaseEncoder("Base64Mime");

        public string Name { get; }

        private BaseEncoder(string name) => this.Name = name;

        public string Encode(byte[] value)
        {
            AssertUtils.AssertNotNull(value);

            if (this == Base64)
            {
                return Base64Utils.ToBase64(value);
            }
            if (this == Base64UrlNP)
            {
                return Base64Utils.ToBase64UrlNP(value);
            }
            return Base64Utils.ToBase64Mime(value);
        }

        public byte[] Decode(string value)
        {
            AssertUtils.AssertNotNull(value);
            if (this == Base64)
            {
                return Base64Utils.FromBase64(value);
            }
            if (this == Base64UrlNP)
            {
                return Base64Utils.FromBase64UrlNP(value);
            }
            return Base64Utils.FromBase64Mime(value);
        }

        public static BaseEncoder Parse(string encoderName)
        {
            AssertUtils.AssertNotNull(encoderName);
            if (Base64.Name.Equals(encoderName, StringComparison.OrdinalIgnoreCase)) return Base64;
            if (Base64UrlNP.Name.Equals(encoderName, StringComparison.OrdinalIgnoreCase)) return Base64UrlNP;
            if (Base64Mime.Name.Equals(encoderName, StringComparison.OrdinalIgnoreCase)) return Base64Mime;

            throw new ArgumentException(nameof(encoderName));
        }
    }
}
