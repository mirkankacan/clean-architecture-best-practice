using CleanArchitecture.Application.Abstracts;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Infrastructure.Authentication;
using CleanArchitecture.Infrastructure.OptionsSetup;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Binding configuration sections to DTOs
            services.ConfigureOptions<JwtOptionsSetup>();
            services.ConfigureOptions<EmailOptionsSetup>();
            services.ConfigureOptions<JwtBearerOptionsSetup>();

            //Authentication
            services.AddAuthentication().AddJwtBearer();
            services.AddAuthorization();

            // Infrastructure Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IJwtProvider, JwtProvider>();
            services.AddScoped<IRoleService, RoleService>();

            return services;
        }
    }
}