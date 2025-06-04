namespace CleanArchitecture.Application.Services
{
    public interface IEmailTemplateService
    {
        string ProcessTemplate(string template, Dictionary<string, object> parameters);

        Task<string> LoadTemplateAsync(string templateName);
    }
}