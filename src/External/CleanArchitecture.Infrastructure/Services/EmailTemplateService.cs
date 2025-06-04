using CleanArchitecture.Application.Services;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class EmailTemplateService : IEmailTemplateService
    {
        private readonly string _templatePath;

        public EmailTemplateService(IConfiguration configuration)
        {
            _templatePath = configuration["EmailTemplates:Path"] ?? "EmailTemplates";
        }

        public string ProcessTemplate(string template, Dictionary<string, object> parameters)
        {
            var result = template;

            foreach (var param in parameters)
            {
                var placeholder = $"{{{param.Key}}}";
                result = result.Replace(placeholder, param.Value?.ToString() ?? string.Empty);
            }

            return result;
        }

        public async Task<string> LoadTemplateAsync(string templateName)
        {
            try
            {
                var filePath = Path.Combine(_templatePath, $"{templateName}.html");
                if (!File.Exists(filePath))
                {
                    filePath = Path.Combine(_templatePath, $"{templateName}.txt");
                }

                if (File.Exists(filePath))
                {
                    return await File.ReadAllTextAsync(filePath);
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}