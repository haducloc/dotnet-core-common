using NetCore.Common.Base;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text;

namespace NetCore.Common.Auth
{
    public class ClaimUtils
    {
        // Claim supports serialization and deserialization but deserialization
        // is not working properly
        // https://github.com/dotnet/corefx/issues/22818

        //NOTES!!!!!: Only Claim Type and Value are serialized and deserialized

        public static IList<Claim> FromBase64(string base64)
        {
            IList<Claim> claims = new List<Claim>();
            byte[] b = BaseEncoder.Base64.Decode(base64);
            using (var r = new BinaryReader(new MemoryStream(b), Encoding.UTF8))
            {
                int count = r.ReadInt32();
                while (count > 0)
                {
                    string type = r.ReadString();
                    string value = r.ReadString();

                    claims.Add(new Claim(type, value));
                    count--;
                }
            }
            return claims;
        }

        public static string ToBase64(IList<Claim> claims)
        {
            using (var o = new MemoryStream())
            {
                using (var w = new BinaryWriter(o, Encoding.UTF8))
                {
                    w.Write(claims.Count);
                    foreach (var claim in claims)
                    {
                        w.Write(claim.Type);
                        w.Write(claim.Value);
                    }
                }
                return BaseEncoder.Base64.Encode(o.ToArray());
            }
        }
    }
}
