using System;

namespace NetCore.Common.Crypto
{
    public class CryptoException : Exception
    {
        public CryptoException()
        {
        }

        public CryptoException(Exception ex) : base(ex.Message, ex)
        {
        }
    }
}
