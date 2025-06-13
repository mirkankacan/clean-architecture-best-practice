using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Infrastructure.Exceptions
{
    public class InvalidCredentialsException : BaseException
    {
        public InvalidCredentialsException() : base("Geçersiz kimlik bilgileri.")
        {
        }
    }
}