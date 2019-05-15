using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NLog;
using Technical_Task.Api.Models;

namespace Technical_Task.Api.CustomExceptionMiddleware
{
    public class ExceptionMiddlewareDevelopment : ExceptionMiddleware
    {
        public ExceptionMiddlewareDevelopment(RequestDelegate next, ILogger logger) : base(next, logger)
        {
        }

        protected override Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
