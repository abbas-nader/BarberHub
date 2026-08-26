using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.File;

public record CreateFileDto(
    string FileName,
    string OriginFileName,
    string Url,
    string ContentType,
    long Size,
    StorageProvider StorageProvider
);