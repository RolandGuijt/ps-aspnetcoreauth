using Globomantics.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace Globomantics.Client
{
    public class ServerAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _HttpClient;

        public ServerAuthenticationStateProvider(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return new AuthenticationState(await GetUser());
        }

        private async ValueTask<ClaimsPrincipal> GetUser()
        {
            var response = await _HttpClient.GetAsync("/Account/User?slide=false");
            if (!response.IsSuccessStatusCode)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var jsonClaims = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonClaims);
            var claims = await response.Content.ReadFromJsonAsync<IEnumerable<UserClaim>>();

            if (claims == null)
            {
                return new ClaimsPrincipal(new ClaimsIdentity());
            }

            var identity = new ClaimsIdentity(
                nameof(ServerAuthenticationStateProvider), "name", "role");

            foreach (var claim in claims)
            {
                identity.AddClaim(new Claim(claim.Type, claim.Value.ToString()));
            }

            return new ClaimsPrincipal(identity);
        }
    }
}
