using System;
using System.Threading.Tasks;
using Jil;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AuthGuard.API.Middleware
{
    public class ErrorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorMiddleware> _logger;

        public ErrorMiddleware(RequestDelegate next, ILogger<ErrorMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError($"AuthGuard.API Exception Message :  [{ex.Message}]");
            _logger.LogError($"AuthGuard.API Exception StackTrace :  [{ex.StackTrace}]");
            
            var result = JSON.Serialize(
                new
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    UserMesssage = "There is a problem",
                    IsSuccess = false,
                });

            return context.Response.WriteAsync(result);
        }
    }
}