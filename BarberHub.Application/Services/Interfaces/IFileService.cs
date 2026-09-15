using BarberHub.Application.DTOs.File;

namespace BarberHub.Application.Services.InterFaces;

public interface IFileService
{
    Task<FileDto> UploadAsync(UploadFileDto uploadFileDto, CancellationToken cancellationToken = default);
    Task<FileDto> GetByIdAsync(long fileId, CancellationToken cancellationToken = default);
    Task<FileDto> DeleteAsync(long fileId, CancellationToken cancellationToken = default);
}