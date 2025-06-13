namespace CleanArchitecture.Domain.Dtos
{
    public sealed record EmailOptionsDto
    {
        public string SmtpHost { get; set; } = string.Empty;
        public int SmtpPort { get; set; } = 587;
        public bool EnableSsl { get; set; } = true;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string DefaultFromEmail { get; set; } = string.Empty;
        public string DefaultFromName { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 30;
    }
}