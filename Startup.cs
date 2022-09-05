using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookShop.Areas.Api.Controllers;
using BookShop.Areas.Api.Services;
using BookShop.Areas.Identity.Data;
using BookShop.Areas.Identity.Services;
using BookShop.Classes;
using BookShop.Models;
using BookShop.Models.Repository;
using BookShop.Models.UnitOfWork;
using BookShop.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
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
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IJwtService, JwtService>();
            services.AddScoped<IConvertDate, ConvertDate>();
            services.AddScoped<IApplicationRoleManager, ApplicationRoleManager>();
            //services.AddScoped<ApplicationUser>();
            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();
            services.AddScoped<ApplicationIdentityErrorDescriber>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<ISmsSender, SmsSender>();
            //services.AddTransient<RoleManager<ApplicationRoles>>();


            services.AddHttpClient();

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

            services.AddApiVersioning(options =>
            {
                options.ApiVersionReader = ApiVersionReader.Combine(new QueryStringApiVersionReader(), new HeaderApiVersionReader("api-version"));
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.Conventions.Controller<SampleV1Controller>().HasApiVersion(new ApiVersion(1, 0));           by this option we can set a version to some controller without ApiVersion Attribute

            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; ;
            })
                .AddJwtBearer(options =>
                {
                    var secretkey = Encoding.UTF8.GetBytes("0123456789ALMTU@");
                    var encryptionkey = Encoding.UTF8.GetBytes("0123456789zxcvbn");

                    var validationParameters = new TokenValidationParameters
                    {
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true, //default : false
                        ValidAudience = "Pangerh-No.com",

                        ValidateIssuer = true, //default : false
                        ValidIssuer = "Pangerh-No.com",

                        TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;
                })
                .AddGoogle(options =>
                {
                    options.ClientId = "721620776951-9jmie7ee9nht9rgke7lr6aqcbn2n1dfn.apps.googleusercontent.com";
                    options.ClientSecret = "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";
                });
            //services.AddMvc(options =>
            //{
            //    var F = services.BuildServiceProvider().GetService<IStringLocalizerFactory>();
            //    var L = F.Create("ModelBindingMessages", "BookShop");
            //    options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
            //     (x) => L["انتخاب یکی از موارد لیست الزامی است."]);

            //});


            // this is configuration for modelState erorrs that transfer modelState erorrs to badRequestObjectresult and implements apiController attribute
            //services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.InvalidModelStateResponseFactory = actionContext =>
            //    {
            //        var erorrs = actionContext.ModelState
            //           .Where(e => e.Value.Errors.Count() != 0)
            //           .Select(e => e.Value.Errors.First().ErrorMessage).ToList();
            //        return new BadRequestObjectResult(erorrs);
            //    };
            //});

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

