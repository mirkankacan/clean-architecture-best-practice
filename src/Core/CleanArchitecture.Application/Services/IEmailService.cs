using CleanArchitecture.Domain.Dtos;

namespace CleanArchitecture.Application.Services
{
    public interface IEmailService
    {
        Task<EmailResult> SendEmailAsync(EmailMessageDto message, CancellationToken cancellationToken = default);

        Task<EmailResult> SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default);

        Task<List<EmailResult>> SendBulkEmailAsync(IEnumerable<EmailMessageDto> messages, CancellationToken cancellationToken = default);

        Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default);
    }
}