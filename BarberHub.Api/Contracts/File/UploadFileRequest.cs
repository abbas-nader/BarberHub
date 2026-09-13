namespace BarberHub.Api.Contracts.File;

public record UploadFileRequest(
    Stream FileStream,
    string OriginFileName,
    string ContentType,
    long Size
);