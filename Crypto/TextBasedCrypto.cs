using NetCore.Common.Base;
using System.Text;

namespace NetCore.Common.Crypto
{
    public abstract class TextBasedCrypto : InitializeObject
    {
        private Encoding _charset;
        public Encoding Charset
        {
            get { return _charset; }
            set
            {
                AssertNotInitialized();
                _charset = value;
            }
        }

        private BaseEncoder _baseEncoder;
        public BaseEncoder BaseEncoder
        {
            get { return _baseEncoder; }
            set
            {
                AssertNotInitialized();
                _baseEncoder = value;
            }
        }
    }
}
