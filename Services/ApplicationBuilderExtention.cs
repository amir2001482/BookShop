using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Services
{
    public static class ApplicationBuilderExtention
    {
        public static IApplicationBuilder UseNodeModule(this IApplicationBuilder app , string path)
        {
            var rootPath = Path.Combine(path, "node_modules");
            var fileProvider = new PhysicalFileProvider(rootPath);
            var fileOption = new StaticFileOptions();
            fileOption.RequestPath = "/node_modules";
            fileOption.FileProvider = fileProvider;
            app.UseStaticFiles(fileOption);
            return app;
        }
    }
}
