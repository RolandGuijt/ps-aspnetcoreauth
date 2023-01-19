using Microsoft.AspNetCore.Authorization;

namespace Globomantics.Authorization
{
    public class YearsOfExperienceRequirement : IAuthorizationRequirement
    {
        public YearsOfExperienceRequirement(int yearsOfExperienceRequired)
        {
            YearsOfExperienceRequired = yearsOfExperienceRequired;
        }
        public int YearsOfExperienceRequired { get; set; }
    }
}
