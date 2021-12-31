This example project serves as an example for using WebApplicationFramework in .Net 6 to perform integration 
testing against a web application that relies on Open ID COnnect for authentication.

References:
* The blog post: [Mocking OIDC Auth while integration testing ASP.Net](http://www.tiernok.com/posts/2022/mocking-oidc-logins-for-integration-tests)
* [Integration tests in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-6.0)

# Running Locally

0. Download or `git clone` this repository
1. Enable [Secret Manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=windows#enable-secret-storage)
   * Open a console to the solution folder
   * Enable user secrets: `dotnet user-secrets init -p .\Sample-OIDC-WebApp\Sample-OIDC-WebApp.csproj`
2. Add the Client Secret for the [demo IdentityServer4](https://demo.identityserver.io/)
   * `dotnet user-secrets set "OIDC:ClientSecret" "secret" -p .\Sample-OIDC-WebApp\Sample-OIDC-WebApp.csproj`
3. Run
   * Visual Studio: Press F5
   * console: `dotnet run -p .\Sample-OIDC-WebApp\Sample-OIDC-WebApp.csproj`

_You can `cd` into the project directory and leave off the `-p ...` on all of these, if you prefer_

To run the tests:
4. Several options:
   * NCrunch (preferred): start NCrunch, it will do the rest
   * Visual Studio: "Tests" menu, "Run All Tests" (or Ctrl + R, A in some configs)
   * Console: `dotnet test` in the solution folder
