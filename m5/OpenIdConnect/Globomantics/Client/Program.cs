using Globomantics.Client;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton(sp =>
{
    var client = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
    client.DefaultRequestHeaders.Add("X-CSRF", "1");
    return client;
});
builder.Services.AddSingleton<IConferenceRepository, ConferenceRepository>();
builder.Services.AddSingleton<IProposalRepository, ProposalRepository>();
builder.Services.AddSingleton<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

await builder.Build().RunAsync();
