using System.Reflection;
using System.Reflection.Metadata;
using CleanArchitecture.Application.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // MediatR Configuration
            var assembly = Assembly.GetExecutingAssembly();

            // MediatR Configuration - Use current assembly
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            // Pipeline Behaviors
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            // FluentValidation
            services.AddValidatorsFromAssembly(assembly);

            return services;
        }
    }
}