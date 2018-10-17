using NetCore.Common.Base;
using NetCore.Common.Crypto;
using NetCore.Common.Entities;
using NetCore.Common.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Common.Services
{
    public abstract class VerificationService : InitializeObject
    {
        private TextDigester _identityDigester;
        public TextDigester IdentityDigester
        {
            get {
                Initialize();
                return _identityDigester;
            }
            set
            {
                AssertNotInitialized();
                _identityDigester = value;
            }
        }

        private PasswordDigester _tokenDigester;

        public PasswordDigester TokenDigester
        {
            get {
                Initialize();
                return _tokenDigester;
            }
            set
            {
                AssertNotInitialized();
                _tokenDigester = value;
            }
        }

        protected override void Init()
        {
            if (this._identityDigester == null) this._identityDigester = new TextDigester { Digester = new DigesterImpl { HashAlgm = HashAlgm.Sha256 } };
            if (this._tokenDigester == null) this._tokenDigester = new PasswordDigester { };
        }

        protected string NextSeries() => UUIDUtils.randomUUID();

        public async Task<string> SaveVerification(string token, string identity, int identityType, string verifyCode, int expiresInSec)
        {
            Initialize();

            var verification = new Verification
            {
                Series = NextSeries(),
                Token = IdentityDigester.Digest(GetTokenData(token, verifyCode)),
                HashIdentity = TokenDigester.Digest(identity.ToLower(CultureUtils.CultureEnglish))
            };

            long curTimeMs = DateUtils.CurrentTimeMillis;

            verification.ExpiresAtUtc = curTimeMs + expiresInSec * 1000L;
            verification.IssuedAtUtc = curTimeMs;

            await DoSaveVerification(verification);
            return verification.Series;
        }

        protected abstract Task DoSaveVerification(Verification verification);

        protected abstract Task<Verification> DoGetVerification(string series);

        public Task<Verification> GetVerification(string series)
        {
            Initialize();
            return DoGetVerification(series);
        }

        public async Task<bool> Verify(string series, string token, string identity, string verifyCode, int expiryLeewayMs)
        {
            Initialize();

            var verification = await DoGetVerification(series);
            if (verification == null)
            {
                return false;
            }
            if (!TokenDigester.Verify(identity.ToLower(CultureUtils.CultureEnglish), verification.HashIdentity))
            {
                return false;
            }
            if (!IdentityDigester.Verify(GetTokenData(token, verifyCode), verification.Token))
            {
                return false;
            }
            if (!DateUtils.IsFutureTime(verification.ExpiresAtUtc.Value, expiryLeewayMs))
            {
                return false;
            }
            return true;
        }

        protected string GetTokenData(string token, string verifyCode)
        {
            return token + verifyCode;
        }
    }
}
