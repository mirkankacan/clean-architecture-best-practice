using CleanArchitecture.WebApi.Middlewares;
using Serilog;

namespace CleanArchitecture.WebApi.Extensions
{
    public static class WebApplicationMiddleware
    {
        public static WebApplication UseWebApiMiddlewares(this WebApplication app)
        {
            // Development Environment Configuration
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();

            app.MapControllers();
            app.Lifetime.ApplicationStopping.Register(Log.CloseAndFlush);

            return app;
        }
    }
}