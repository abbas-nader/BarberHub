using BarberHub.Domain.Enums;

namespace BarberHub.Application.DTOs.File;

public record UploadFileDto(
   Stream FileStream,
    string OriginFileName,
    string ContentType,
    long Size
);