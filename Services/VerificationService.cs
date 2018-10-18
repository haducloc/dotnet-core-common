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
            get
            {
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
            get
            {
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

            long curTimeMs = DateUtils.CurrentTimeMillis;
            long expiresAtUtc = curTimeMs + expiresInSec * 1000L;
            identity = identity.ToLower(CultureUtils.CultureEnglish);

            var verification = new Verification
            {
                Series = NextSeries(),
                Token = IdentityDigester.Digest(GetTokenData(token, identity, verifyCode, expiresAtUtc)),
                HashIdentity = TokenDigester.Digest(identity)
            };

            verification.ExpiresAtUtc = expiresAtUtc;
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
            identity = identity.ToLower(CultureUtils.CultureEnglish);

            if (!TokenDigester.Verify(identity, verification.HashIdentity))
            {
                return false;
            }
            if (!IdentityDigester.Verify(GetTokenData(token, identity, verifyCode, verification.ExpiresAtUtc.Value), verification.Token))
            {
                return false;
            }
            if (!DateUtils.IsFutureTime(verification.ExpiresAtUtc.Value, expiryLeewayMs))
            {
                return false;
            }
            return true;
        }

        protected string GetTokenData(string token, string identity, string verifyCode, long expiresAtUtc)
        {
            return token + identity + verifyCode + expiresAtUtc;
        }
    }
}
