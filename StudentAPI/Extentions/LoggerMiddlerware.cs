using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace StudentAPI.Extentions
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class LoggerMiddlerware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggerMiddlerware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("MyLogger");

        }

        public Task Invoke(HttpContext httpContext)
        {
            _logger.LogInformation("Sample data inside our Logger Middleware");

            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggerMiddlerwareExtensions
    {
        public static IApplicationBuilder UseLoggerMiddlerware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddlerware>();
        }
    }
}
