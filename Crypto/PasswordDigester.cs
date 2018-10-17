using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NetCore.Common.Utils;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace NetCore.Common.Crypto
{
    public class PasswordDigester : TextDigester
    {
        private int _saltSize;
        public int SaltSize
        {
            get { return _saltSize; }
            set
            {
                AssertNotInitialized();
                _saltSize = value;
            }
        }

        private int _iterationCount;
        public int IterationCount
        {
            get { return _iterationCount; }
            set
            {
                AssertNotInitialized();
                _iterationCount = value;
            }
        }

        private int _keySize;
        public int KeySize
        {
            get { return _keySize; }
            set
            {
                AssertNotInitialized();
                _keySize = value;
            }
        }

        readonly RandomNumberGenerator random = new RNGCryptoServiceProvider();
        readonly SecretKeyGenerator secretKeyGenerator = new SecretKeyGenerator(KeyDerivationPrf.HMACSHA256);

        protected override void Init()
        {
            KeySize = ValueUtils.ValueOrDefault(KeySize, 32);
            SaltSize = ValueUtils.ValueOrDefault(SaltSize, KeySize);
            IterationCount = Math.Max(IterationCount, 5000);

            BaseEncoder = ValueUtils.ValueOrDefault(BaseEncoder, NetCore.Common.Base.BaseEncoder.Base64);
        }

        public new string Digest(string password)
        {
            Initialize();
            AssertUtils.AssertNotNull(password);

            byte[] salt = new byte[SaltSize];
            random.GetBytes(salt);

            byte[] secKey = secretKeyGenerator.Generate(password, salt, IterationCount, KeySize);
            return BaseEncoder.Encode(ArrayUtils.Append(salt, secKey));
        }

        public new bool Verify(string password, string digested)
        {
            Initialize();
            AssertUtils.AssertNotNull(password);
            AssertUtils.AssertNotNull(digested);

            byte[] dg = BaseEncoder.Decode(digested);
            AssertUtils.AssertTrue(dg.Length > SaltSize);

            byte[] salt = new byte[SaltSize];
            byte[] secKey = new byte[dg.Length - SaltSize];
            ArrayUtils.Copy(dg, salt, secKey);

            byte[] computedSecKey = secretKeyGenerator.Generate(password, salt, IterationCount, KeySize);
            return computedSecKey.SequenceEqual(secKey);
        }
    }
}
