using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using NetCore.Common.Utils;

namespace NetCore.Common.Crypto
{
    public class SecretKeyGenerator
    {
        public KeyDerivationPrf Algorithm { get; private set; } = KeyDerivationPrf.HMACSHA256;

        public SecretKeyGenerator() { }

        public SecretKeyGenerator(KeyDerivationPrf algorithm) => Algorithm = AssertUtils.AssertNotNull(algorithm);

        public byte[] Generate(string password, byte[] salt, int iterationCount, int keySize)
        {
            return KeyDerivation.Pbkdf2(password, salt, this.Algorithm, iterationCount, keySize);
        }
    }
}
