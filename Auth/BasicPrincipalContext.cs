using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace NetCore.Common.Auth
{
    public class BasicPrincipalContext : PrincipalContext<BasicAuthOptions>
    {
        public string Password { get; }

        public string UserName { get; }

        public BasicPrincipalContext(HttpContext context, AuthenticationScheme scheme, BasicAuthOptions options, string userName, string password) 
            : base(context, scheme, options, null)
        {
            this.UserName = userName;
            this.Password = password;
        }
    }
}
