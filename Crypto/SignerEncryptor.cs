using NetCore.Common.Base;
using NetCore.Common.Utils;
using System;

namespace NetCore.Common.Crypto
{
    public class SignerEncryptor : InitializeObject, Encryptor
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

        private Digester _Signer;
        public Digester Signer
        {
            get { return _Signer; }
            set
            {
                AssertNotInitialized();
                _Signer = value;
            }
        }

        protected override void Init()
        {
            AssertUtils.AssertNotNull(_Signer);
        }

        public byte[] Encrypt(byte[] message)
        {
            this.Initialize();
            AssertUtils.AssertNotNull(message);

            byte[] encrypted = (this.Encryptor != null) ? this.Encryptor.Encrypt(message) : message;
            byte[] digested = this.Signer.Digest(encrypted);

            if (this.Signer.GetDigestSize() > 0)
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

            int digestLength = this.Signer.GetDigestSize();
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

            if (!this.Signer.Verify(parsedMsg, digested))
            {
                throw new CryptoException("The message was tampered.");
            }
            return (this.Encryptor != null) ? this.Encryptor.Decrypt(parsedMsg) : parsedMsg;
        }
    }
}
