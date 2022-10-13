using BookShop.Areas.Identity.Data;
using BookShop.Classes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookShop.Areas.Identity.Services
{
    public static class IdentityServicesRegistry
    {
        public static void AddCustomIdentityServices(this IServiceCollection services, SiteSettings siteSettings)
        {
            services.AddIdentityOptions();
            services.AddDynamicPersmission();
            services.AddCustomAuthentication();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<ApplicationIdentityErrorDescriber>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISmsSender, SmsSender>();
        }

        public static void UseCustomIdentityServices(this IApplicationBuilder app)
        {
            app.UseAuthentication();
        }

    }
}
