using Globomantics.Authorization;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

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
    .AddCookie(o => {
        o.Events = new CookieAuthenticationEvents
        {
            OnValidatePrincipal = (context =>
            {
                return Task.CompletedTask;
            })
        };
        o.ExpireTimeSpan = new TimeSpan(1, 0, 0);
        o.Cookie.MaxAge = new TimeSpan(1, 0, 0);
       
    }) 
    .AddGoogle(o =>
    {
        o.ClientId = "686977813024-d9i87jqqovj5tu5luks9rk8gl33ck3rb.apps.googleusercontent.com";
        o.ClientSecret = "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";
    });

builder.Services.AddAuthorization(o => 
{
    o.AddPolicy("IsOrganizer", o => o.RequireRole("organizer"));
    o.AddPolicy("IsSpeaker", o => o.RequireRole("speaker"));
    o.AddPolicy("CanAddConference", o => o.RequireClaim("permission", "addconference"));
    o.AddPolicy("YearsOfExperience", o => 
        o.AddRequirements(new YearsOfExperienceRequirement(10)));
    o.AddPolicy("EditProposal", o =>
        o.AddRequirements(new ProposalRequirement()));
});

builder.Services.AddSingleton<IAuthorizationHandler, 
    YearsOfExperienceAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler,
    ProposalAuthorizationHandler>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("default", "{controller=Conference}/{action=Index}/{id?}");

app.Run();
