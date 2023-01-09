using Duende.IdentityServer.AspNetIdentity;
using IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

public class ProfileService: ProfileService<ApplicationUser>
{
	public ProfileService(UserManager<ApplicationUser> userManager, 
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory)
        : base(userManager, claimsFactory)
	{
	}

    protected override async Task<ClaimsPrincipal> GetUserClaimsAsync(ApplicationUser user)
    {
        var principal = await base.GetUserClaimsAsync(user);
        var identity = (ClaimsIdentity)principal.Identity;

        var existingClaim = identity.Claims.FirstOrDefault(c => c.Type == "careerstarted");
        if (existingClaim != null)
            return principal;

        identity.AddClaim(new Claim("careerstarted", user.CareerStarted.ToString()));
        return principal;
    }
}