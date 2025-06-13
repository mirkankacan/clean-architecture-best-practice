using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Infrastructure.Exceptions
{
    public class ConfigurationException : BaseException
    {
        public ConfigurationException(string message) : base(message)
        {
        }
    }
}