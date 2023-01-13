using Globomantics.Client;
using Globomantics.Client.ApiServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton(sp => new HttpClient { 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<IConferenceApiService, ConferenceApiService>();
builder.Services.AddSingleton<IProposalApiService, ProposalApiService>();
builder.Services.AddSingleton<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

await builder.Build().RunAsync();
