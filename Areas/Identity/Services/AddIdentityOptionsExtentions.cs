using BookShop.Areas.Identity.Data;
using BookShop.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Services
{
    public static class AddIdentityOptionsExtentions
    {
        public static IServiceCollection AddIdentityOptions(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                options =>
                {
                    //Configure Password
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;

                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    options.User.RequireUniqueEmail = true;

                    options.SignIn.RequireConfirmedEmail = true;

                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(20);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                })
             .AddEntityFrameworkStores<BookShopContext>()
             .AddErrorDescriber<ApplicationIdentityErrorDescriber>()
             .AddDefaultTokenProviders();


            return services;
        }
    }
}
