using System.Diagnostics;
using JwtAuthDotNet9.Exceptions;
using JwtAuthDotNet9.Models.BaseModels;

namespace JwtAuthDotNet9.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
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
            catch (AppException ex)
            {
                await WriteResponse(context, ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                await WriteResponse(
                    context,
                    StatusCodes.Status500InternalServerError,
                    "Beklenmeyen bir hata oluştu"
                );
            }
        }

        private static async Task WriteResponse(
            HttpContext context,
            int statusCode,
            string message)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
           
            var response = ApiResponse<object>.Fail(message);

            await context.Response.WriteAsJsonAsync(response);
        }
    }

}
