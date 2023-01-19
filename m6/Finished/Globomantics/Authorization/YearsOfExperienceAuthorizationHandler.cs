using Microsoft.AspNetCore.Authorization;

namespace Globomantics.Authorization
{
    public class YearsOfExperienceAuthorizationHandler : 
        AuthorizationHandler<YearsOfExperienceRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            YearsOfExperienceRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "careerstarted"))
            {
                return Task.CompletedTask;
            }

            var careerStarted = DateTimeOffset.Parse(
                context.User.FindFirst(c => c.Type == "careerstarted")
                    .Value
            );

            var yearsOfExperience = 
                Math.Round((DateTimeOffset.Now - careerStarted)
                    .TotalDays / 365);

            if (yearsOfExperience >= requirement.YearsOfExperienceRequired)
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
