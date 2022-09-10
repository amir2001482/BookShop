using BookShop.Areas.Api.Class;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BookShop.Areas.Api.Middlewares
{
    public static class CustomExeptionsHandlerMiddlewareExtentions
    {
        public static IApplicationBuilder UseCustomExeptionHandler(this IApplicationBuilder builder)
        {

            return builder.UseMiddleware<CustomExeptionsHandlerMiddleware>();
        }
    }
    public class CustomExeptionsHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostingEnvironment _env;

        public CustomExeptionsHandlerMiddleware(RequestDelegate next , IHostingEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            var Message = new List<string>();
            var httpStatusCode = HttpStatusCode.InternalServerError;
            var apiResultStatusCode = StatusCodeEnum.ServerError;
            try
            {
                await _next(context);
            }
            catch(Exception exeption)
            {
                if (_env.IsDevelopment())
                {
                    var error = new Dictionary<string, string>()
                    {
                        ["Exeption"] = exeption.Message,
                        ["StackTrace"] = exeption.StackTrace,
                    };
                    var json = JsonConvert.SerializeObject(error);
                    Message.Add(json);
                }
                else
                {
                    Message.Add("خطایی رخ داده است ");
                }

                await WriteToResponeAsync();
            }

            async Task WriteToResponeAsync()
            {
                var apiResult = new ApiResult(false, apiResultStatusCode, Message);
                var jsonResult = JsonConvert.SerializeObject(apiResult);
                context.Response.StatusCode = (int)httpStatusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(jsonResult);
            }
            
        }
    }
}
