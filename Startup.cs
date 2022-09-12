using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BookShop.Areas.Api.Class;
using BookShop.Areas.Api.Controllers;
using BookShop.Areas.Api.Middlewares;
using BookShop.Areas.Api.Services;
using BookShop.Areas.Identity.Data;
using BookShop.Areas.Identity.Services;
using BookShop.Classes;
using BookShop.Exceptions;
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
        public IConfiguration Configuration { get; }
        private readonly SiteSettings _siteSettings;
        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
            _siteSettings = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

     

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));
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

            services.AddPaging(options =>
            {
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
                //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; 
            })
                .AddJwtBearer(options =>
                {
                    var secretkey = Encoding.UTF8.GetBytes(_siteSettings.jwtSettings.Secretkey);
                    var encryptionkey = Encoding.UTF8.GetBytes(_siteSettings.jwtSettings.EncryptKey);

                    var validationParameters = new TokenValidationParameters
                    {
                        RequireSignedTokens = true,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretkey),

                        RequireExpirationTime = true,
                        ValidateLifetime = true,

                        ValidateAudience = true, //default : false
                        ValidAudience = _siteSettings.jwtSettings.Audience,

                        ValidateIssuer = true, //default : false
                        ValidIssuer = _siteSettings.jwtSettings.Issuer,

                        TokenDecryptionKey = new SymmetricSecurityKey(encryptionkey)
                    };

                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;
                    options.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if(context.Exception != null)
                            {
                                if (context.Exception != null)
                                    throw new AppException(StatusCodeEnum.UnAuthorized, "Authentication failed.", HttpStatusCode.Unauthorized, context.Exception, null);
                                
                            }
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = async context =>
                        {
                            var userRepository = context.HttpContext.RequestServices.GetRequiredService<IApplicationUserManager>();

                            var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
                            if (claimsIdentity.Claims?.Any() != true)
                                context.Fail("This token has no claims.");

                            var securityStamp = claimsIdentity.FindFirstValue(new ClaimsIdentityOptions().SecurityStampClaimType);
                            if (!securityStamp.HasValue())
                                context.Fail("This token has no secuirty stamp");

                            var userId = claimsIdentity.GetUserId<string>();
                            var user = await userRepository.GetUserAsync(context.Principal);

                            if (user.SecurityStamp != securityStamp)
                                context.Fail("Token secuirty stamp is not valid.");

                            if (!user.IsActive)
                                context.Fail("User is not active.");
                        },
                        OnChallenge = context =>
                        {
                            if (context.AuthenticateFailure != null)
                                throw new AppException(StatusCodeEnum.UnAuthorized, "Authenticate failure.", HttpStatusCode.Unauthorized, context.AuthenticateFailure, null);
                            throw new AppException(StatusCodeEnum.UnAuthorized, "You are unauthorized to access this resource.", HttpStatusCode.Unauthorized);
                        }
                    };
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
            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), builder =>
            {
                builder.UseCustomExeptionHandler();  // custom Middleware for handele exeptions errors
            });
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), builder =>
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    app.UseHsts();
                }
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "node_modules")),
                RequestPath = "/" + "node_modules",
            });

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

