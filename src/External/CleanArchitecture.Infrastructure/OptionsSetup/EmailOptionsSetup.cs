using CleanArchitecture.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.OptionsSetup
{
    public sealed class EmailOptionsSetup : IConfigureOptions<EmailOptionsDto>
    {
        private readonly IConfiguration _configuration;

        public EmailOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(EmailOptionsDto options)
        {
            _configuration.GetSection("EmailConfiguration").Bind(options);
        }
    }
}