using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookShop.Areas.Identity.Data;
using BookShop.Areas.Identity.Services;
using BookShop.Classes;
using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.UnitOfWork;
using BookShop.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using ReflectionIT.Mvc.Paging;

namespace BookShop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<BookShopContext>();
            services.AddTransient<BooksRepository>();
            services.AddTransient<ConvertDate>();
            services.AddScoped<IConvertDate, ConvertDate>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            //services.AddScoped<ApplicationUser>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<ApplicationIdentityErrorDescriber>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISmsSender, SmsSender>();
            services.AddHttpClient();
            //services.AddTransient<RoleManager<ApplicationRoles>>();

            //services.AddMvc(options =>
            //{
            //    var F = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
            //    var L = F.Create("ModelBindingMessages", "BookShop");
            //    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            //     (x) => L["انتخاب یکی از موارد لیست الزامی است."]);

            //});
            services.AddPaging(options => {
                options.ViewName = "Bootstrap4";
                options.HtmlIndicatorDown = "<i class='fa fa-sort-amount-down'></i>";
                options.HtmlIndicatorUp = "<i class='fa fa-sort-amount-up'></i>";
            });
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(2);
                options.Cookie.HttpOnly = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                    RequestPath = "/" + "node_modules",
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseSession();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                                    name: "areas",
                                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                                    name: "default",
                                    template: "{controller=Home}/{action=Index}/{id?}");
                
            });
        }
    }
}
