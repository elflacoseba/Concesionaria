using Concesionaria.Application.Exceptions;
using FluentValidation;
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
            catch (ValidationException validationException)
            {
                _logger.LogError(validationException, "Excepción de validación");
                var customEx = new CustomValidationException(validationException.Errors);
                await HandleCustomValidationExceptionAsync(context, customEx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción no controlada");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleCustomValidationExceptionAsync(HttpContext context, CustomValidationException exception)
        {
            var code = HttpStatusCode.BadRequest;
            var result = JsonSerializer.Serialize(new
            {
                Title = exception.Message,
                Status = (int)code,
                Instance = context.Request.Path,
                Errors = exception.Errors
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode code = HttpStatusCode.InternalServerError;
            ProblemDetails problem;

            if (exception is KeyNotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            problem = new ProblemDetails
            {
                Title = "Ha ocurrido un problema.",
                Detail = exception.Message,
                Status = (int)code,
                Instance = context.Request.Path
            };

            var defaultResult = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(defaultResult);
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
