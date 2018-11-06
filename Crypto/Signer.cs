using NetCore.Common.Base;
using NetCore.Common.Utils;
using System;

namespace NetCore.Common.Crypto
{
    public class Signer : InitializeObject, Encryptor
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

        private Digester _Digester;
        public Digester Digester
        {
            get { return _Digester; }
            set
            {
                AssertNotInitialized();
                _Digester = value;
            }
        }

        protected override void Init()
        {
            AssertUtils.AssertNotNull(_Digester);
        }

        public byte[] Encrypt(byte[] message)
        {
            this.Initialize();
            AssertUtils.AssertNotNull(message);

            byte[] encrypted = (this.Encryptor != null) ? this.Encryptor.Encrypt(message) : message;
            byte[] digested = this.Digester.Digest(encrypted);

            if (this.Digester.GetDigestSize() > 0)
            {
                return ArrayUtils.Append(digested, encrypted);
            }
            else
            {
                throw new Exception("Not implemented");
            }
        }

        public byte[] Decrypt(byte[] message)
        {
            this.Initialize();
            AssertUtils.AssertNotNull(message);

            byte[] digested = null;
            byte[] parsedMsg = null;

            int digestLength = this.Digester.GetDigestSize();
            if (digestLength > 0)
            {
                digested = new byte[digestLength];
                AssertUtils.AssertTrue(message.Length > digestLength, "message is invalid.");

                parsedMsg = new byte[message.Length - digestLength];
                ArrayUtils.Copy(message, digested, parsedMsg);
            }
            else
            {
                throw new Exception("Not implemented");
            }

            if (!this.Digester.Verify(parsedMsg, digested))
            {
                throw new CryptoException("The message was tampered.");
            }
            return (this.Encryptor != null) ? this.Encryptor.Decrypt(parsedMsg) : parsedMsg;
        }
    }
}
