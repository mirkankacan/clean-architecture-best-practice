namespace CleanArchitecture.Domain.Dtos
{
    public sealed record EmailConfigurationDto(
       string SmtpHost,
       string Username,
       string Password,
       string DefaultFromEmail,
       int SmtpPort = 587,
       bool EnableSsl = true,
       string DefaultFromName = "",
       int TimeoutSeconds = 30
   );
}