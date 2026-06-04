using AureaBeautyClinic.Shared.Common;
using AureaBeautyClinic.Shared.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace AureaBeautyClinic.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            var (statusCode, message) = exception switch
            {
                AuthenticationException => (StatusCodes.Status401Unauthorized, exception.Message),
                EmailAlreadyRegisteredException => (StatusCodes.Status409Conflict, exception.Message),
                _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.")
            };

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            IEnumerable<string>? errors = _environment.IsDevelopment() && statusCode == StatusCodes.Status500InternalServerError
                ? new[] { exception.Message }
                : null;

            var response = ApiResponse<object>.Fail(message, errors);
            await context.Response.WriteAsJsonAsync(response, ct);
            return true;
        }
    }
}
