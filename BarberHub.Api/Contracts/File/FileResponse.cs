namespace BarberHub.Api.Contracts.File;

public record FileResponse(
    long Id,
    string FileName,
    string OriginFileName,
    string Url,
    string ContentType,
    long Size
);