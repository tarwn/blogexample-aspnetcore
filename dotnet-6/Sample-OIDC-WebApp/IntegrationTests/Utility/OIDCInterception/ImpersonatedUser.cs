using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace IntegrationTests.Utility.OIDCInterception
{
    // see ../CustomWebApplicationfactory for instructions

    public abstract class ImpersonatedUser : AuthenticationHandler<ImpersonatedAuthenticationSchemeOptions>
    {
        public ImpersonatedUser(IOptionsMonitor<ImpersonatedAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var claims = new List<Claim>();
            Options.Configure(claims);
            var identity = new ClaimsIdentity(claims, Options.OriginalScheme);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Options.OriginalScheme);

            var result = AuthenticateResult.Success(ticket);

            return Task.FromResult(result);
        }
    }

    public class ImpersonatedAuthenticationSchemeOptions : AuthenticationSchemeOptions
    {
        public string OriginalScheme { get; set; } = "";
        public Action<List<Claim>> Configure { get; set; } = (_) => { };
    }
}
