using NetCore.Common.Base;
using NetCore.Common.Utils;
using System.Text;

namespace NetCore.Common.Crypto
{
    public class TextDigester : TextBasedCrypto
    {
        private Digester _digester;
        public Digester Digester
        {
            get { return _digester; }
            set
            {
                AssertNotInitialized();
                _digester = value;
            }
        }

        protected override void Init()
        {
            Charset = ValueUtils.ValueOrDefault(Charset, Encoding.UTF8);
            BaseEncoder = ValueUtils.ValueOrDefault(BaseEncoder, BaseEncoder.Base64);

            AssertUtils.AssertNotNull(Digester);
        }

        public string Digest(string message) {
            Initialize();
            AssertUtils.AssertNotNull(message);

            return BaseEncoder.Encode(Digester.Digest(Charset.GetBytes(message)));
        }

        public bool Verify(string message, string digested)
        {
            Initialize();
            AssertUtils.AssertNotNull(message);
            AssertUtils.AssertNotNull(digested);

            return Digester.Verify(Charset.GetBytes(message), BaseEncoder.Decode(digested));
        }
    }
}
