using Duende.IdentityServer.Models;

namespace IdentityProvider;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource("globomantics", "Globomantics specific information", 
                new[] { "role", "permission", "careerstarted" })
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("globomanticsapi"),
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
        {
            new ApiResource("globomantics", "Globomantics APIs")
            {
                Scopes = {"globomanticsapi"}
            }
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "globomantics_web",
                ClientSecrets = { 
                    new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                AlwaysIncludeUserClaimsInIdToken = false,
                RequireConsent = false,

                RedirectUris = { "https://localhost:5001/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:5001/signout-oidc",
                PostLogoutRedirectUris = { 
                    "https://localhost:5001/signout-callback-oidc" },
               
                AllowedScopes = { "openid", "email", "globomantics", 
                    "globomanticsapi", "profile" }
            },
        };
}
