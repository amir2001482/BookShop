using BookShop.Areas.Identity.Services;
using BookShop.Classes;
using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public static class ApplicationServicesRegistry
    {
        public static void AddCustomApplicationServices(this IServiceCollection services)
        {

            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ConvertDate>();
            services.AddTransient<IConvertDate, ConvertDate>();
            services.AddTransient<BooksRepository>();
            services.AddTransient<BookShopContext>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddLocalization(options => { options.ResourcesPath = "Resources"; });
            services.AddMvc(options =>
            {
                var F = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
                var L = F.Create("ModelBindingMessages", "BookShop");
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                 (x) => L["انتخاب یکی از موارد لیست الزامی است."]);

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
    }
}
