using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookShop.Classes
{
    public class HappyBirthDayHandler :  AuthorizationHandler<HappyBirthDayRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HappyBirthDayRequirement requirement)
        {
           if(!context.User.HasClaim(c=>c.Type==ClaimTypes.DateOfBirth))
            {
                return Task.CompletedTask;
            }

            var DateOfBirth = Convert.ToDateTime(context.User.FindFirstValue(ClaimTypes.DateOfBirth));

            if(DateOfBirth.ToString("MM/dd")==DateTime.Now.ToString("MM/dd"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
