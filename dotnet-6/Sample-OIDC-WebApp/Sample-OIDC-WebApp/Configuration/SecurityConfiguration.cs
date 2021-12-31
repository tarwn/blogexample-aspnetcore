using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Sample_OIDC_WebApp.Configuration
{
    public static class SecurityConfiguration
    {
        public const string LocalSessionIdClaim = "SessionId";
        public const string CookieScheme = "Cookies";
        public const string OIDCScheme = "oidc";

        public static void AddOIDCAuthentication(this IServiceCollection services)
        {
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

            services.AddOptions<OpenIdConnectOptions>(OIDCScheme)
                .Configure<IOptionsMonitor<OIDCSettings>>((options, settings) =>
                {
                    options.Authority = settings.CurrentValue.Authority;
                    options.ClientId = settings.CurrentValue.ClientId;
                    options.ClientSecret = settings.CurrentValue.ClientSecret;
                });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieScheme;
                options.DefaultChallengeScheme = OIDCScheme;
            })
                .AddCookie(CookieScheme)
                .AddOpenIdConnect(OIDCScheme, options =>
                {
                    options.ResponseType = "code";
                    options.UsePkce = true;

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");


                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.SaveTokens = true;

                    options.Events.OnTicketReceived = ctx => {
                        // pretend we have a more refined way to generate local sessions
                        var fakeSessionId = Random.Shared.NextInt64();
                        var identity = new ClaimsIdentity(new [] { 
                            new Claim(SecurityConfiguration.LocalSessionIdClaim, fakeSessionId.ToString())
                        });
                        ctx.Principal!.AddIdentity(identity);
                        return Task.CompletedTask;
                    };
                });
        }
    }
}
