using Microsoft.AspNetCore.Authentication;
using NetCore.Common.Base;
using NetCore.Common.Utils;
using System;
using System.IO;
using System.Text;

namespace NetCore.Common.Auth
{
    public class AuthProperties : AuthenticationProperties
    {
        private double? _expiresInSec;
        public double? ExpiresInSec
        {
            get
            {
                return this._expiresInSec;
            }
            set
            {
                if (value != null)
                {
                    this.ExpiresUtc = DateTime.UtcNow.AddSeconds(value.Value);
                } else
                {
                    this.ExpiresUtc = null;
                }
                this._expiresInSec = value;
            }
        }

        public AuthProperties FromBase64(string base64)
        {
            byte[] b = BaseEncoder.Base64.Decode(base64);
            using (var r = new BinaryReader(new MemoryStream(b), Encoding.UTF8))
            {
                this.IsPersistent = r.ReadBoolean();
                this.AllowRefresh = r.ReadBoolOpt();
                this.RedirectUri = r.ReadStringOpt();
                this.ExpiresInSec = r.ReadDoubleOpt();
            }
            return this;
        }

        public string ToBase64()
        {
            using (var o = new MemoryStream())
            {
                using (var w = new BinaryWriter(o, Encoding.UTF8))
                {
                    w.Write(this.IsPersistent);
                    w.WriteOpt(this.AllowRefresh);
                    w.WriteOpt(this.RedirectUri);
                    w.WriteOpt(this.ExpiresInSec);
                }
                return BaseEncoder.Base64.Encode(o.ToArray());
            }
        }
    }
}
