using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Presentation.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            // Controller Configuration
            services.AddControllerConfiguration();

            // JSON Configuration
            services.AddJsonConfiguration();

            return services;
        }

        private static IServiceCollection AddControllerConfiguration(this IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.ModelValidatorProviders.Clear();
            })
            .AddApplicationPart(Assembly.GetExecutingAssembly()) // Bu satırı değiştirdik
            .ConfigureApiBehaviorOptions(options =>
            {
                // Custom model validation response
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    return new BadRequestObjectResult(new
                    {
                        Message = "Validation failed",
                        Errors = errors
                    });
                };
            });

            return services;
        }

        private static IServiceCollection AddJsonConfiguration(this IServiceCollection services)
        {
            services.Configure<JsonOptions>(options =>
            {
                // Configure JSON serialization options
                options.SerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
                options.SerializerOptions.WriteIndented = false;
                options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                // Handle reference loops
                options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            return services;
        }
    }
}