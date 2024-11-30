namespace DocumentAPI.ErrorHandlers
{
    public class ErrorBase(
        int statusCode,
        string message
    ) : Exception
    {
        public int StatusCode { get; set; } = statusCode;

        public  string Message { get; set; } = message;
    }
}
