using CleanArchitecture.WebApi.Middlewares;

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

            app.UseMiddlewareExtensions();
            app.UseHttpsRedirection();

            // Routing Configuration
            app.UseAuthorization();
            app.MapControllers();

            return app;
        }
    }
}