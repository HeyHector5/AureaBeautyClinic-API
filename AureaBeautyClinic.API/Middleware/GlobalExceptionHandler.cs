using AureaBeautyClinic.Shared.Common;
using Microsoft.AspNetCore.Diagnostics;

namespace AureaBeautyClinic.API.Middleware
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken ct)
        {
            _logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = ApiResponse<object>.Fail(
                "An unexpected error occurred. Please try again later.",
                new[] { exception.Message }
            );

            await context.Response.WriteAsJsonAsync(response, ct);
            return true;
        }
    }
}
