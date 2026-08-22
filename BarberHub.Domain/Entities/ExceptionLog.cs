namespace BarberHub.Domain.Entities;

public class ExceptionLog
{
    public string? Id { get; private set; }
    public string ExceptionType { get; private set; } = null!;
    public string Message { get; private set; } = null!;
    public string? StackTrace { get; private set; }
    public string? Source { get; private set; }
    public string? RequestPath { get; private set; }
    public string? RequestMethod { get; private set; }
    public int StatusCode { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }

    private ExceptionLog()
    {
    }

    public static ExceptionLog CreateByException(
        Exception exception,
        int statusCode,
        string? requestPath = null,
        string? requestMethod = null)
    {
        return new ExceptionLog
        {
            ExceptionType = exception.GetType().Name,
            Message = exception.Message,
            StackTrace = exception.StackTrace,
            Source = exception.Source,
            RequestPath = requestPath,
            RequestMethod = requestMethod,
            StatusCode = statusCode,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }
}