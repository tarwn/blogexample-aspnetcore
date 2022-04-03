using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(options => { });

// Configure the SPA static files to come from a new parallel folder
builder.Services.AddSpaStaticFiles(config =>
{
    config.RootPath = "reactroot";
});

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

// in production, the SPA static files will be on disk and served here for lower latency
// in development this will do nothing, as the static files from the SPA are not built to disk
//  those calls will make it through to the UseSpa below and get live, in-memory verisons of the files
//  with HMR snippets added to them
app.UseSpaStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
});

// in development this will start the web server + proxy calls to it
// in production, it will simply return "index.html" for all un-routed calls that make it this far
app.UseSpa(builder =>
{
    builder.Options.DefaultPage = "/reactroot/index.html";
    builder.Options.SourcePath = "../reactapp";
    // this is designed to work with Webpack 4 only, we can hack it by outputting the hardcoded 🙄 output message
    //  it is watching for that indicates the server is running
    builder.UseReactDevelopmentServer("start:vs");
});

app.Run();
