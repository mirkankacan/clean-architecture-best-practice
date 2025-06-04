namespace CleanArchitecture.Domain.Dtos
{
    public sealed record EmailResult(
       bool IsSuccess,
       string? ErrorMessage = null,
       string? MessageId = null
   )
    {
        public static EmailResult Success(string? messageId = null) =>
            new(true, null, messageId);

        public static EmailResult Failure(string errorMessage) =>
            new(false, errorMessage, null);
    }
}