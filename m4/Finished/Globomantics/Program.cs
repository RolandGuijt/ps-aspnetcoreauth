using Globomantics.Repositories;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddSingleton<IConferenceRepository, ConferenceRepository>();
builder.Services.AddSingleton<IProposalRepository, ProposalRepository>();

builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    o.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddOpenIdConnect(options =>
    {
        options.Authority = "https://localhost:5000";

        options.ClientId = "globomantics_web";
        //Store in application secrets
        options.ClientSecret = "49C1A7E1-0C79-4A89-A3D6-A37998FB86B0";
        options.Scope.Add(JwtClaimTypes.Email);
        options.Scope.Add("globomantics");
        options.SaveTokens = true;
        options.ResponseType = "code";
        options.GetClaimsFromUserInfoEndpoint = false;

        //options.ClaimActions.MapUniqueJsonKey("careerstarted",
        //               "careerstarted");
        //options.ClaimActions.MapUniqueJsonKey("birthdate",
        //               "birthdate");
        //options.ClaimActions.MapUniqueJsonKey("Role", "Role");
        //options.ClaimActions.MapUniqueJsonKey("permission", "permission");


        options.Events = new OpenIdConnectEvents
        {
            OnTokenResponseReceived = t =>
            {
                Console.WriteLine(t.TokenEndpointResponse.IdToken);
                return Task.CompletedTask;
            }
        };

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
