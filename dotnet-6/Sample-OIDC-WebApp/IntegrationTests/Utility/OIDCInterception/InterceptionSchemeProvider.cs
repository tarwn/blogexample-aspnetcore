using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Sample_OIDC_WebApp.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTests.Utility.OIDCInterception
{
    /// <summary>
    /// This is the keystone of the integration test example: here we replace the built-in AuthenticationSchemeProvider
    /// with one that will intercept requests for our "OIDC" scheme provider and instead find the registered one for
    /// our "InterceptedScheme"
    /// 
    /// This is a minimnal shim to ensure that (1) we don't weaken our actual production code, and (2) we retain the flexibility
    /// to setup that intercepted auth provider at an level of granulartity (test fixture or individual test) needed for our
    /// individual tests
    /// </summary>
    public class InterceptOidcAuthenticationSchemeProvider : AuthenticationSchemeProvider
    {
        public const string InterceptedScheme = "InterceptedScheme";

        public InterceptOidcAuthenticationSchemeProvider(IOptions<AuthenticationOptions> options)
            : base(options)
        {
        }

        protected InterceptOidcAuthenticationSchemeProvider(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes)
            : base(options, schemes)
        {
        }

        public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
        {
            // if this matches the OIDC scheme, call the auth provider for whichever fake one we setup for the client
            if (name == SecurityConfiguration.OIDCScheme)
            {
                return base.GetSchemeAsync(InterceptedScheme);
            }

            return base.GetSchemeAsync(name);
        }
    }

    public class AutoFailSchemeProvider : AuthenticationSchemeProvider
    {
        public const string AutoFailScheme = "AutoFail";

        public AutoFailSchemeProvider(IOptions<AuthenticationOptions> options)
            : base(options)
        {
        }

        protected AutoFailSchemeProvider(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes)
            : base(options, schemes)
        {
        }

        public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
        {
            // if this matches the OIDC scheme, call the auth provider for whichever fake one we setup for the client
            if (name == SecurityConfiguration.OIDCScheme)
            {
                return base.GetSchemeAsync(AutoFailScheme);
            }

            return base.GetSchemeAsync(name);
        }
    }
}
