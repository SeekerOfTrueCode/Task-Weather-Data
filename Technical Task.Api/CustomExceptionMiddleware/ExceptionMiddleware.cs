using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;
using Technical_Task.Api.Models;

namespace Technical_Task.Api.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        protected readonly RequestDelegate Next;
        protected readonly ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            Next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await Next(httpContext);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, $"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        protected virtual Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware."
            }.ToString());
        }
    }
}
