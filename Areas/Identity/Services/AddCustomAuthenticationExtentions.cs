using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Areas.Identity.Services
{
    public static class AddCustomAuthenticationExtentions
    {
        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication()
              .AddGoogle(options =>
              {
                  options.ClientId = "315654760867-d01fsd0fb847vft0fbo6hvbgqghrt5ph.apps.googleusercontent.com";
                  options.ClientSecret = "F7rY4md1LciG24O_4J_RAPct";
              });
              //.AddYahoo(options =>
              //{
              //    options.ClientId = "dj0yJmk9aWxnZVZNTGVwVXhWJnM9Y29uc3VtZXJzZWNyZXQmc3Y9MCZ4PWQz";
              //    options.ClientSecret = "9d68b57943e8035cd0771f49d2b54af10797eb4e";
              //});

            return services;
        }
    }
}
