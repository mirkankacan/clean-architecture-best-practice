using CleanArchitecture.Domain.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace CleanArchitecture.Infrastructure.OptionsSetup
{
    public sealed class JwtOptionsSetup : IConfigureOptions<JwtOptionsDto>
    {
        private readonly IConfiguration _configuration;

        public JwtOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(JwtOptionsDto options)
        {
            _configuration.GetSection("JwtConfiguration").Bind(options);
        }
    }
}