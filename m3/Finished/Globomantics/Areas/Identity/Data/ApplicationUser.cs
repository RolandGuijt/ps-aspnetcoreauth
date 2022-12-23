using Microsoft.AspNetCore.Identity;

namespace Globomantics.Areas.Identity.Data
{
    public class ApplicationUser: IdentityUser
    {
        public DateTime CareerStarted { get; set; }
    }
}
