using Microsoft.AspNetCore.Builder;
using Technical_Task.Api.CustomExceptionMiddleware;

namespace Technical_Task.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
        public static void ConfigureCustomExceptionMiddlewareDevelopment(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddlewareDevelopment>();
        }
    }
}
