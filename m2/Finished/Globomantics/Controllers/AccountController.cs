using Globomantics.Models;
using Globomantics.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Globomantics.Controllers;

public class AccountController : Controller
{
    private readonly IUserRepository userRepository;

    public AccountController(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public IActionResult Login(string returnUrl = "/")
    {
        return View(new LoginModel { ReturnUrl = returnUrl });
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model)
    {
        var user = userRepository.GetByUsernameAndPassword(model.Username, model.Password);
        if (user == null)
            return Unauthorized();

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FavoriteColor", user.FavoriteColor)
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
            new AuthenticationProperties { IsPersistent = model.RememberLogin });

        return LocalRedirect(model.ReturnUrl);
    }

    public IActionResult LoginWithGoogle(string returnUrl = "/")
    {
        var props = new AuthenticationProperties
        {
            RedirectUri = Url.Action("GoogleLoginCallback"),
            Items =
            {
                { "returnUrl", returnUrl }
            }
        };
        return Challenge(props, GoogleDefaults.AuthenticationScheme);
    }

    public async Task<IActionResult> GoogleLoginCallback()
    {
        // read google identity from google's cookie
        var result = await HttpContext.AuthenticateAsync(
            GoogleDefaults.AuthenticationScheme);
        
        if (result.Principal == null)
            throw new Exception("Could not create a principal");
        var externalClaims = result.Principal.Claims.ToList();

        var subjectIdClaim = externalClaims.FirstOrDefault(
            x => x.Type == ClaimTypes.NameIdentifier);

        if (subjectIdClaim == null)
            throw new Exception("Could not extract a subject id claim");

        var subjectValue = subjectIdClaim.Value;

        var user = userRepository.GetByGoogleId(subjectValue);

        if (user == null)
            throw new Exception("Local user not found");

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("FavoriteColor", user.FavoriteColor)
        };

        var identity = new ClaimsIdentity(claims, 
            CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme, 
            principal);

        return LocalRedirect(result.Properties?.Items["returnUrl"] 
            ?? "/");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/");
    }
}
