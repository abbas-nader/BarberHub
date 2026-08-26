namespace BarberHub.Application.DTOs.File;

public record FileDto(
    string FileName,
    string OriginFileName,
    string Url,
    string ContentType,
    long Size
    );