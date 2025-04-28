using System.Net;
using TaskManagementSystem.Exceptions;

namespace TaskManagementSystem.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Custom exception occurred: {Message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                var customEx = new CustomException("An unexpected error occurred.", (int)HttpStatusCode.InternalServerError);
                await HandleExceptionAsync(httpContext, customEx);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var response = new ErrorResponse(
                exception.StatusCode, 
                exception.Message, 
                exception.InnerException?.Message
            );

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
