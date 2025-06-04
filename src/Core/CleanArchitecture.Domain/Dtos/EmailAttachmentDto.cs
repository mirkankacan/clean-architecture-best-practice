namespace CleanArchitecture.Domain.Dtos
{
    public sealed record EmailAttachmentDto(
     string FileName,
     byte[] Content,
     string ContentType = "application/octet-stream",
     bool IsInline = false,
     string? ContentId = null
 );
}