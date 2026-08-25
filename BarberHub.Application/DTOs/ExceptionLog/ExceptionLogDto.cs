namespace BarberHub.Application.DTOs.ExceptionLog;

public record ExceptionLogDto(
    string Id,
    string ExceptionType,
    string Message,
    string? StackTrace,
    string? Source,
    string? RequestPath,
    string? RequestMethod,
    int StatusCode,
    DateTimeOffset CreatedAt);
