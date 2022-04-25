using AuthGuard.API.Middleware;
using Microsoft.AspNetCore.Builder;

namespace AuthGuard.API.Extensions
{
    public static class ApplicationBuilder
    {
        public static void UseSwaggerUIBuilder(this IApplicationBuilder builder)
        {
            builder.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AuthGuard.API v1");
                c.DocumentTitle = "AuthGuard API";
                c.RoutePrefix = string.Empty;
            });
        }

        public static void UseMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorMiddleware>();
        }
    }
}