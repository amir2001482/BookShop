using BookShop.Classes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Services
{
    public static class AddCustomPoliciesExtentions
    {
        public static IServiceCollection AddCustomPolicies(this IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationHandler, HappyBirthDayHandler>();
            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AccessToUsersManager", policy => policy.RequireRole("مدیر سایت"));
                options.AddPolicy("HappyBirthDay", policy => policy.Requirements.Add(new HappyBirthDayRequirement()));
                options.AddPolicy("Atleast18", policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
            });

            return services;
        }
    }
}
