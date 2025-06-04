using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("EmailConfiguration");
            var emailConfig = new EmailConfigurationDto(
                SmtpHost: section["SmtpHost"] ?? throw new InvalidOperationException("SmtpHost gerekli"),
                Username: section["Username"] ?? throw new InvalidOperationException("Username gerekli"),
                Password: section["Password"] ?? throw new InvalidOperationException("Password gerekli"),
                DefaultFromEmail: section["DefaultFromEmail"] ?? throw new InvalidOperationException("DefaultFromEmail gerekli"),
                SmtpPort: int.TryParse(section["SmtpPort"], out var port) ? port : 587,
                EnableSsl: bool.TryParse(section["EnableSsl"], out var ssl) ? ssl : true,
                TimeoutSeconds: int.TryParse(section["TimeoutSeconds"], out var timeout) ? timeout : 30,
                DefaultFromName: section["DefaultFromName"] ?? ""
            );
            services.AddSingleton(emailConfig);

            // Infrastructure Services
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IEmailTemplateService, EmailTemplateService>();

            return services;
        }
    }
}