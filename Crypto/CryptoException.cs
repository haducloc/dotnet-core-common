using System;

namespace NetCore.Common.Crypto
{
    public class CryptoException : Exception
    {
        public CryptoException()
        {
        }

        public CryptoException(string message) : base(message)
        {
        }

        public CryptoException(Exception ex) : base(ex.Message, ex)
        {
        }
    }
}
