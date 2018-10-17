using Microsoft.AspNetCore.Authentication;
using System;

namespace NetCore.Common.Auth
{
    public static class BasicAuthExtensions
    {
        public static AuthenticationBuilder AddBasicAuth(this AuthenticationBuilder builder)
            => builder.AddBasicAuth(BasicAuthDefaults.AuthenticationScheme);

        public static AuthenticationBuilder AddBasicAuth(this AuthenticationBuilder builder, string authenticationScheme)
            => builder.AddBasicAuth(authenticationScheme, null);

        public static AuthenticationBuilder AddBasicAuth(this AuthenticationBuilder builder, Action<BasicAuthOptions> configureOptions)
            => builder.AddBasicAuth(BasicAuthDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddBasicAuth(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<BasicAuthOptions> configureOptions)
            => builder.AddScheme<BasicAuthOptions, BasicAuthHandler>(authenticationScheme, configureOptions);
    }
}
