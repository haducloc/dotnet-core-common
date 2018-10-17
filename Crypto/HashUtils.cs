using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Common.Crypto
{
    public class HashUtils
    {
        public static string Md5(string data)
        {
            return new TextDigester { Digester = new DigesterImpl { HashAlgm = HashAlgm.Md5 } }.Digest(data);
        }

        public static string Sha256(string data)
        {
            return new TextDigester { Digester = new DigesterImpl { HashAlgm = HashAlgm.Sha256 } }.Digest(data);
        }

        public static bool Sha256(string data, string hash)
        {
            return new TextDigester { Digester = new DigesterImpl { HashAlgm = HashAlgm.Sha256 } }.Verify(data, hash);
        }
    }
}
