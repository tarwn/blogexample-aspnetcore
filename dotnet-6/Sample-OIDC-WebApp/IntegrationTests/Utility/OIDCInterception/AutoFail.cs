using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace IntegrationTests.Utility.OIDCInterception
{

    public class AutoFail : AuthenticationHandler<AutoFailOptions>
    {
        public const string SchemeName = "AutoFail";

        public AutoFail(IOptionsMonitor<AutoFailOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.Fail("AutoFail"));
        }

    }

    public class AutoFailOptions : AuthenticationSchemeOptions
    { }

}
