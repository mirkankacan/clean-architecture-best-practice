using System.Net;
using CleanArchitecture.WebApi.Constants;
using FluentValidation;

namespace CleanArchitecture.WebApi.Middlewares
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly Serilog.ILogger _logger = Serilog.Log.ForContext<ExceptionMiddleware>();

        public ExceptionMiddleware()
        {
            // No need to inject ILogger since we're using Serilog's static Log
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync(context, ex); // Fixed: was context.ex
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = ex switch
            {
                ValidationException validationEx => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ErrorMessages.ValidationFailed,
                    Errors = validationEx.Errors.Select(e => e.ErrorMessage).Distinct().ToArray()
                },
                ArgumentException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = ErrorMessages.InvalidArgument
                },
                UnauthorizedAccessException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = ErrorMessages.UnauthorizedAccess
                },
                KeyNotFoundException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Message = ErrorMessages.ResourceNotFound
                },
                TimeoutException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.RequestTimeout,
                    Message = ErrorMessages.RequestTimeout
                },
                _ => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = ErrorMessages.InternalServerError
                }
            };

            response.StatusCode = errorResponse.StatusCode;
            await response.WriteAsJsonAsync(errorResponse);
        }

        private Task LogExceptionAsync(HttpContext context, Exception ex)
        {
            var request = context.Request;

            _logger.Error(ex,
                "{RemoteIpAddress} adresinden gelen {Method} {Path} isteği işlenirken istisna oluştu. TraceId: {TraceId}",
                context.Connection.RemoteIpAddress, // Fixed: parameter order
                request.Method,
                request.Path,
                context.TraceIdentifier);

            return Task.CompletedTask;
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public string[]? Errors { get; set; }
    }
}