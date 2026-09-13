namespace BarberHub.Api.Contracts.File;

public record UploadFileRequest(
    IFormFile File,
    string OriginFileName
);