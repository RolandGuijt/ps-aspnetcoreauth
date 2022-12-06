using Globomantics;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IConferenceRepository, ConferenceRepository>();
builder.Services.AddScoped<IProposalRepository, ProposalRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    //o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;           
})
    .AddCookie()
    .AddCookie(ExternalAuthenticationDefaults.AuthenticationScheme)
    .AddGoogle(o =>
    {
        o.SignInScheme = ExternalAuthenticationDefaults.AuthenticationScheme;
        o.ClientId = "455500451200-g7ijj2lsfi3hfualk2il7plolrbtpd3a.apps.googleusercontent.com";
        o.ClientSecret = "5ExwgELgP2CntPxVye11PZ_c";
    });

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Conference}/{action=Index}/{id?}");
});

app.Run();
