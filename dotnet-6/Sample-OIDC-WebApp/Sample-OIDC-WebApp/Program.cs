using Sample_OIDC_WebApp.Configuration;

var builder = WebApplication.CreateBuilder(args);

// add authentication against Identity Server
// please stop hard-coding OIDC settings in demo code
builder.Services.Configure<OIDCSettings>(builder.Configuration.GetSection("OIDC"));
builder.Services.AddOIDCAuthentication();

// add authorization policies
builder.Services.AddAuthorization(options => {
    // InteractiveUser: must be auth'd and have a user id
    options.AddPolicy(Policies.InteractiveUser, policy => {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim(SecurityConfiguration.LocalSessionIdClaim);
    });
    // (Default) NoneShallPass - ensures an explicit authorize is specified for all endpoints (reduce accidents)
    options.AddPolicy(Policies.NoneShallPass, policy => policy.RequireAssertion(_ => false));   
    options.DefaultPolicy = options.GetPolicy(Policies.NoneShallPass)!;
    options.FallbackPolicy = options.DefaultPolicy;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


// this addition makes the Program class visible to the integration test project
public partial class Program { }