using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NetCore.Common.Auth
{
    public class BasicAuthHandler : AuthenticationHandler<BasicAuthOptions>
    {
        public BasicAuthHandler(IOptionsMonitor<BasicAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected new BasicAuthEvents Events
        {
            get => (BasicAuthEvents)base.Events;
            set => base.Events = value;
        }

        protected override Task<object> CreateEventsAsync() => Task.FromResult<object>(new BasicAuthEvents());

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorization = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorization))
            {
                return AuthenticateResult.NoResult();
            }

            string credentialBase64 = null;
            if (authorization.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            {
                credentialBase64 = authorization.Substring("Basic ".Length).Trim();
            }
            if (string.IsNullOrEmpty(credentialBase64))
            {
                return AuthenticateResult.NoResult();
            }

            string credentialString = null;
            try
            {
                byte[] credentialBytes = Convert.FromBase64String(credentialBase64);
                credentialString = Encoding.GetEncoding("ISO-8859-1").GetString(credentialBytes);
            }
            catch (Exception)
            {
                return AuthenticateResult.NoResult();
            }

            var credentialSplit = credentialString.Split(new[] { ':' }, 2);
            if (credentialSplit.Length != 2)
            {
                return AuthenticateResult.NoResult();
            }

            var context = new BasicPrincipalContext(this.Context, this.Scheme, this.Options, credentialSplit[0], credentialSplit[1]);
            await this.Events.Validate(context);

            if (context.Principal == null)
            {
                return AuthenticateResult.NoResult();
            }
            return AuthenticateResult.Success(new AuthenticationTicket(context.Principal, context.Properties, this.Scheme.Name));
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            this.Response.StatusCode = StatusCodes.Status401Unauthorized;
            this.Response.Headers.Append(HeaderNames.WWWAuthenticate, $"Basic realm=\"{this.Options.RealmName}\"");

            return Task.CompletedTask;
        }
    }
}
