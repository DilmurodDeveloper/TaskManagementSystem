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
                _logger.LogError($"Custom Exception: {ex.Message}");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An unexpected error occurred: {ex.Message}");
                await HandleExceptionAsync(httpContext, new CustomException("An unexpected error occurred.", (int)HttpStatusCode.InternalServerError));
            }
        }

        private Task HandleExceptionAsync(HttpContext context, CustomException exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var response = new ErrorResponse(exception.StatusCode, exception.Message);
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
