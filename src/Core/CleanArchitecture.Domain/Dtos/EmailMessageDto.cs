namespace CleanArchitecture.Domain.Dtos
{
    public sealed record EmailMessageDto(
      string To,
      string Subject,
      string Body,
      string[]? Cc = null,
      string[]? Bcc = null,
      bool IsHtml = true,
      List<EmailAttachmentDto>? Attachments = null,
      string? From = null,
      string? FromDisplayName = null,
      Dictionary<string, string>? Headers = null
  );
}