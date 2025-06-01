namespace CleanArchitecture.WebApi.Middlewares
{
    public sealed class ErrorResult : ErrorStatusCode
    {
        public string Message { get; set; }
    }

    public class ErrorStatusCode
    {
        public int StatusCode { get; set; }
    }

    public sealed class ValidationErrorDetails : ErrorStatusCode
    {
        public IEnumerable<string> Errors { get; set; } = new List<string>();
    }
}