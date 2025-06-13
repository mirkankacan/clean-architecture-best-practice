using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Infrastructure.Exceptions
{
    public sealed class UserNotFoundException : BaseException
    {
        public UserNotFoundException(string email) : base($"'{email}' email adresli kullanıcı bulunamadı.")
        {
        }
    }
}