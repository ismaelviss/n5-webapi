using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace n5.webApi.Middleware
{
    public class OptionsMiddleware
    {
        readonly RequestDelegate next;

        public OptionsMiddleware(RequestDelegate nextRequest)
        {
            next = nextRequest;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.Contains("OPTIONS"))
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                context.Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
                context.Response.Headers.Add("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
                await context.Response.WriteAsync("");
            }
            else
                await next(context);
        }
    }

    public static class OptionsMiddlewareExtension
    {
        public static IApplicationBuilder UseOptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<OptionsMiddleware>();
        }
    }
}