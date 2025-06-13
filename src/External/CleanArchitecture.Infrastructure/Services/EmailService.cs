using System.Net;
using System.Net.Mail;
using CleanArchitecture.Application.Services;
using CleanArchitecture.Domain.Dtos;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.Services
{
    public sealed class EmailService : IEmailService
    {
        private readonly EmailOptionsDto _config;

        public EmailService(IOptions<EmailOptionsDto> config)
        {
            _config = config.Value;
        }

        public async Task<EmailResult> SendEmailAsync(EmailMessageDto message, CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = CreateSmtpClient();
                using var mailMessage = CreateMailMessage(message);

                await client.SendMailAsync(mailMessage, cancellationToken);

                return EmailResult.Success();
            }
            catch (Exception ex)
            {
                return EmailResult.Failure(ex.Message);
            }
        }

        public async Task<EmailResult> SendEmailAsync(string to, string subject, string body, bool isHtml = true, CancellationToken cancellationToken = default)
        {
            var message = new EmailMessageDto(to, subject, body, IsHtml: isHtml);
            return await SendEmailAsync(message, cancellationToken);
        }

        public async Task<List<EmailResult>> SendBulkEmailAsync(IEnumerable<EmailMessageDto> messages, CancellationToken cancellationToken = default)
        {
            var results = new List<EmailResult>();
            var semaphore = new SemaphoreSlim(5); // Limit concurrent sends

            var tasks = messages.Select(async message =>
            {
                await semaphore.WaitAsync(cancellationToken);
                try
                {
                    return await SendEmailAsync(message, cancellationToken);
                }
                finally
                {
                    semaphore.Release();
                }
            });

            results.AddRange(await Task.WhenAll(tasks));
            return results;
        }

        public async Task<bool> TestConnectionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                using var client = CreateSmtpClient();
                // Test connection by trying to connect and authenticate
                await Task.Run(() => client.Send(new MailMessage(_config.DefaultFromEmail, _config.DefaultFromEmail, "Test", "Test")), cancellationToken);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private SmtpClient CreateSmtpClient()
        {
            Console.WriteLine($"SMTP Host: {_config.SmtpHost}");
            Console.WriteLine($"SMTP Port: {_config.SmtpPort}");
            Console.WriteLine($"Enable SSL: {_config.EnableSsl}");
            Console.WriteLine($"Username: {_config.Username}");
            Console.WriteLine($"Has Password: {!string.IsNullOrEmpty(_config.Password)}");
            var client = new SmtpClient(_config.SmtpHost, _config.SmtpPort)
            {
                EnableSsl = _config.EnableSsl,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_config.Username, _config.Password),
                Timeout = _config.TimeoutSeconds * 1000
            };

            return client;
        }

        private MailMessage CreateMailMessage(EmailMessageDto message)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(
                    message.From ?? _config.DefaultFromEmail,
                    message.FromDisplayName ?? _config.DefaultFromName),
                Subject = message.Subject,
                Body = message.Body,
                IsBodyHtml = message.IsHtml
            };

            mailMessage.To.Add(message.To);

            // Add CC recipients
            if (message.Cc != null)
            {
                foreach (var cc in message.Cc)
                    mailMessage.CC.Add(cc);
            }

            // Add BCC recipients
            if (message.Bcc != null)
            {
                foreach (var bcc in message.Bcc)
                    mailMessage.Bcc.Add(bcc);
            }

            // Add custom headers
            if (message.Headers != null)
            {
                foreach (var header in message.Headers)
                    mailMessage.Headers.Add(header.Key, header.Value);
            }

            // Add attachments
            if (message.Attachments != null)
            {
                foreach (var attachment in message.Attachments)
                {
                    var mailAttachment = new Attachment(
                        new MemoryStream(attachment.Content),
                        attachment.FileName,
                        attachment.ContentType);

                    if (attachment.IsInline && !string.IsNullOrEmpty(attachment.ContentId))
                    {
                        mailAttachment.ContentId = attachment.ContentId;
                        mailAttachment.ContentDisposition!.Inline = true;
                    }

                    mailMessage.Attachments.Add(mailAttachment);
                }
            }

            return mailMessage;
        }
    }
}