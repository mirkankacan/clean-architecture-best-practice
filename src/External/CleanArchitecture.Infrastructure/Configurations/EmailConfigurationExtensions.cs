using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Infrastructure.Exceptions;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Configurations
{
    public static class EmailConfigurationExtensions
    {
        public static EmailConfigurationDto GetEmailConfiguration(this IConfiguration configuration)
        {
            var section = configuration.GetSection("EmailConfiguration");

            return new EmailConfigurationDto(
                SmtpHost: section["SmtpHost"] ?? throw new ConfigurationException("EmailConfiguration:SmtpHost gerekli"),
                Username: section["Username"] ?? throw new ConfigurationException("EmailConfiguration:Username gerekli"),
                Password: section["Password"] ?? throw new ConfigurationException("EmailConfiguration:Password gerekli"),
                DefaultFromEmail: section["DefaultFromEmail"] ?? throw new ConfigurationException("EmailConfiguration:DefaultFromEmail gerekli"),
                SmtpPort: int.TryParse(section["SmtpPort"], out var port) ? port : 587,
                EnableSsl: bool.TryParse(section["EnableSsl"], out var ssl) ? ssl : true,
                TimeoutSeconds: int.TryParse(section["TimeoutSeconds"], out var timeout) ? timeout : 30,
                DefaultFromName: section["DefaultFromName"] ?? ""
            );
        }
    }
}