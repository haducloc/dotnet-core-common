using NetCore.Common.Base;
using NetCore.Common.Utils;
using System.Text;

namespace NetCore.Common.Crypto
{
    public class TextEncryptor : TextBasedCrypto
    {
        private Encryptor _Encryptor;

        public Encryptor Encryptor
        {
            get { return _Encryptor; }
            set
            {
                AssertNotInitialized();
                _Encryptor = value;
            }
        }

        protected override void Init()
        {
            Charset = ValueUtils.ValueOrDefault(Charset, Encoding.UTF8);
            BaseEncoder = ValueUtils.ValueOrDefault(BaseEncoder, BaseEncoder.Base64);

            AssertUtils.AssertNotNull(Encryptor);
        }

        public string Encrypt(string message) {
            Initialize();
            AssertUtils.AssertNotNull(message);

            return BaseEncoder.Encode(Encryptor.Encrypt(Charset.GetBytes(message)));
        }

        public string Decrypt(string message)
        {
            Initialize();
            AssertUtils.AssertNotNull(message);

            return Charset.GetString(Encryptor.Decrypt(BaseEncoder.Decode(message)));
        }
    }
}
