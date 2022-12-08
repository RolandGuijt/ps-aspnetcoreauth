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
    //.AddTwitter( ..)
    .AddGoogle(o =>
    {
        o.SignInScheme = ExternalAuthenticationDefaults.AuthenticationScheme;
        o.ClientId = "686977813024-d9i87jqqovj5tu5luks9rk8gl33ck3rb.apps.googleusercontent.com";
        o.ClientSecret = "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";
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
