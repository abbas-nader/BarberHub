namespace BarberHub.Application.DTOs;

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
