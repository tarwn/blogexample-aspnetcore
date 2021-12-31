using IntegrationTests.Utility.OIDCInterception;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample_OIDC_WebApp.Configuration;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace IntegrationTests.Utility
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // by default the OIDC auth scheme is intercepted and an auto-failing handler is returned,
                //  this ensures we don't accidentally call the discovery URL
                // CreateLoggedInClient will replace this provider with a different interceptor, last one in wins
                services.AddTransient<IAuthenticationSchemeProvider, AutoFailSchemeProvider>();
                services.AddAuthentication(AutoFailSchemeProvider.AutoFailScheme)
                    .AddScheme<AutoFailOptions, AutoFail>(AutoFailSchemeProvider.AutoFailScheme, null);
            });
        }

        public HttpClient CreateLoggedInClient<T>(WebApplicationFactoryClientOptions options, int sessionId)
            where T : GeneralUser
        {
            return CreateLoggedInClient<T>(options, list =>
            {
                list.Add(new Claim("sessionid", sessionId.ToString()));
            });
        }


        /// <summary>
        /// This configures the "InterceptedScheme" to return a particular type of user, which we can also enrich with extra
        /// parameters/options for use in custom Claims
        /// </summary>
        /// <remarks>
        /// Adding a new user type:
        ///   1. Add a minimal implementation of ImpersonatedUser to be the user class (example below)
        ///   2. Add a new helper method with appropriate args typed to the new class (example above)
        /// </remarks>
        private HttpClient CreateLoggedInClient<T>(WebApplicationFactoryClientOptions options, Action<List<Claim>> configure)
            where T : ImpersonatedUser
        {
            var client = this
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureTestServices(services =>
                    {
                        // configure the intercepting provider
                        services.AddTransient<IAuthenticationSchemeProvider, InterceptOidcAuthenticationSchemeProvider>();

                        // Add a "Test" scheme in to process the auth instead, using the provided user type
                        services.AddAuthentication(InterceptOidcAuthenticationSchemeProvider.InterceptedScheme)
                            .AddScheme<ImpersonatedAuthenticationSchemeOptions, T>("InterceptedScheme", options =>
                            {
                                options.OriginalScheme = SecurityConfiguration.OIDCScheme;
                                options.Configure = configure;
                            });
                    });
                })
                .CreateClient(options);

            return client;
        }

        public class GeneralUser : ImpersonatedUser
        {
            public GeneralUser(IOptionsMonitor<ImpersonatedAuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
                : base(options, logger, encoder, clock)
            { }
        }

    }
}
