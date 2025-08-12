using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace Concesionaria.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            if (exception is KeyNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            var problem = new ProblemDetails
            {
                Title = "Ha ocurrido un problema.",
                Detail = exception.Message,
                Status = (int)code,
                Instance = context.Request.Path
            };

            var result = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
