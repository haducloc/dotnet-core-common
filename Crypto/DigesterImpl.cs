using NetCore.Common.Base;
using NetCore.Common.Utils;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace NetCore.Common.Crypto
{
    public class DigesterImpl : InitializeObject, Digester, IDisposable
    {
        private HashAlgm? _hashAlgm;
        public HashAlgm? HashAlgm
        {
            get { return _hashAlgm; }
            set
            {
                AssertNotInitialized();
                _hashAlgm = value;
            }
        }

        private HashAlgorithm hashEngine;

        protected override void Init()
        {
            AssertUtils.AssertNotNull(HashAlgm);
            this.hashEngine = CreateHashEngine(HashAlgm.Value);
        }

        public byte[] Digest(byte[] message)
        {
            Initialize();

            AssertUtils.AssertNotNull(message);
            return this.hashEngine.ComputeHash(message);
        }

        public bool Verify(byte[] message, byte[] digested)
        {
            Initialize();

            AssertUtils.AssertNotNull(message);
            AssertUtils.AssertNotNull(digested);

            byte[] msgDigested = this.hashEngine.ComputeHash(message);
            return digested.SequenceEqual(msgDigested);
        }

        public int GetDigestSize()
        {
            Initialize();
            return this.hashEngine.HashSize / 8;
        }

        public new void Dispose() => this.hashEngine?.Dispose();

        static HashAlgorithm CreateHashEngine(HashAlgm hashAlgm)
        {
            switch (hashAlgm)
            {
                case NetCore.Common.Crypto.HashAlgm.Md5:
                    return MD5.Create();
                case NetCore.Common.Crypto.HashAlgm.Sha256:
                    return SHA256.Create();
                case NetCore.Common.Crypto.HashAlgm.Sha384:
                    return SHA384.Create();
                case NetCore.Common.Crypto.HashAlgm.Sha512:
                    return SHA512.Create();
                default:
                    throw new ArgumentException(nameof(hashAlgm));
            }
        }
    }
}
