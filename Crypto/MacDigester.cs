using NetCore.Common.Base;
using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace NetCore.Common.Crypto
{
    public class MacDigester : InitializeObject, Digester, IDisposable
    {
        private byte[] _secret;
        private MacAlgm? _macAlgm;

        public byte[] Secret
        {
            private get { return _secret; }
            set
            {
                AssertNotInitialized();
                if (value != null)
                {
                    _secret = (byte[])value.Clone();
                }
            }
        }

        public MacAlgm? MacAlgm
        {
            get { return _macAlgm; }
            set
            {
                AssertNotInitialized();
                _macAlgm = value;
            }
        }

        private HMAC macEngine;

        protected override void Init()
        {
            AssertUtils.AssertNotNull(Secret);
            AssertUtils.AssertNotNull(MacAlgm);

            this.macEngine = CreateMacEngine();

            Array.Clear(Secret, 0, Secret.Length);
        }

        public override void Dispose()
        {
            macEngine?.Dispose();
        }

        public int GetDigestSize()
        {
            Initialize();
            return macEngine.HashSize / 8;
        }

        public byte[] Digest(byte[] message)
        {
            Initialize();

            AssertUtils.AssertNotNull(message);
            return macEngine.ComputeHash(message);
        }

        public bool Verify(byte[] message, byte[] digested)
        {
            Initialize();

            AssertUtils.AssertNotNull(message);
            AssertUtils.AssertNotNull(digested);

            byte[] hashed = macEngine.ComputeHash(message);
            return hashed.SequenceEqual(digested);
        }

        HMAC CreateMacEngine()
        {
            switch (MacAlgm.Value)
            {
                case NetCore.Common.Crypto.MacAlgm.HmacMd5:
                    return new HMACMD5(this.Secret);
                case NetCore.Common.Crypto.MacAlgm.HmacSha256:
                    return new HMACSHA256(this.Secret);
                case NetCore.Common.Crypto.MacAlgm.HmacSha384:
                    return new HMACSHA384(this.Secret);
                case NetCore.Common.Crypto.MacAlgm.HmacSha512:
                    return new HMACSHA512(this.Secret);
                default:
                    throw new ArgumentException(nameof(MacAlgm));
            }
        }
    }
}
