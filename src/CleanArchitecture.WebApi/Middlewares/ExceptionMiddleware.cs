using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Persistance.Context;
using FluentValidation;

namespace CleanArchitecture.WebApi.Middlewares
{
    public sealed class ExceptionMiddleware : IMiddleware
    {
        private readonly AppDbContext _context;

        public ExceptionMiddleware(AppDbContext context)
        {
            _context = context;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionToDatabaseAsync(ex, context.Request);

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            if (ex.GetType() == typeof(ValidationException))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";

                var errorResponse = new
                {
                    statusCode = 400,
                    errors = ((ValidationException)ex).Errors.Select(e => e.ErrorMessage).Distinct().ToArray()
                };

                return context.Response.WriteAsJsonAsync(errorResponse);
            }

            return context.Response.WriteAsJsonAsync(new
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            });
        }

        private async Task LogExceptionToDatabaseAsync(Exception ex, HttpRequest request)
        {
            ErrorLog errorLog = new ErrorLog
            {
                ErrorMessage = ex.Message,
                StackTrace = ex.StackTrace,
                RequestPath = request.Path,
                RequestMethod = request.Method
            };
            await _context.Set<ErrorLog>().AddAsync(errorLog);
            await _context.SaveChangesAsync();
        }
    }
}