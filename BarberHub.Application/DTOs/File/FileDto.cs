namespace BarberHub.Application.DTOs.File;

public record FileDto(
    long Id,
    string FileName,
    string OriginFileName,
    string Url,
    string ContentType,
    long Size
    );