using System;
using BookShop.Areas.Identity.Data;
using BookShop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BookShop.Areas.Identity.IdentityHostingStartup))]
namespace BookShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BookShopIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BookShopIdentityContextConnection")));

                //services.AddDefaultIdentity<BookShopUser>()
                //.AddEntityFrameworkStores<BookShopIdentityContext>();
                services.AddIdentity<ApplicationUser, ApplicationRoles>()
                .AddErrorDescriber<ApplicationIdentityErrorDescriber>()
                .AddEntityFrameworkStores<BookShopIdentityContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI();
                services.Configure<IdentityOptions>(option =>
                {
                    option.Password.RequireDigit = true;
                    option.Password.RequiredLength = 7;
                    option.Password.RequireLowercase = false;
                    option.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                    option.SignIn.RequireConfirmedEmail = true;
                    option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(3);
                    option.Lockout.MaxFailedAccessAttempts = 3;



                });

                    
                
                

                
                
                
                
            });
        }
    }
}