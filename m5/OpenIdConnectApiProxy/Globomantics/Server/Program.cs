using Duende.Bff.Yarp;
using Globomantics.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddBff(o => o.ManagementBasePath = "/account")
    .AddRemoteApis()
    .AddServerSideSessions();

builder.Services.AddAuthentication(o => 
{ 
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    o.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie(o =>
    {
        o.Cookie.Name = "__Host-spa";
        o.Cookie.SameSite = SameSiteMode.Strict;
    })
    .AddOpenIdConnect(options =>
     {
         options.Authority = "https://localhost:5000";

         options.ClientId = "globomantics_web";
         //Store in application secrets
         options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
         options.Scope.Clear();
         options.Scope.Add("openid");
         options.Scope.Add("profile");
         options.Scope.Add("email");
         options.Scope.Add("globomantics");
         options.Scope.Add("globomanticsapi");
         options.SaveTokens = true;
         options.ResponseType = "code";
         options.GetClaimsFromUserInfoEndpoint = true;

         options.ClaimActions.MapUniqueJsonKey("careerstarted", "careerstarted");
         options.ClaimActions.MapUniqueJsonKey("birthdate", "birthdate");
         options.ClaimActions.MapUniqueJsonKey("role", "role");
         options.ClaimActions.MapUniqueJsonKey("permission", "permission");
     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthentication();

app.UseRouting();
app.UseBff();
app.UseAuthorization();

app.MapRazorPages();
app.MapDefaultControllerRoute();
app.UseEndpoints(e => {
    e.MapBffManagementEndpoints();
    e.MapRemoteBffApiEndpoint("/api", "https://localhost:5002")
        .RequireAccessToken();
});
app.MapFallbackToFile("index.html");

app.Run();
