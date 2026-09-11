using BarberHub.Application.Constants;
using BarberHub.Application.DTOs.File;
using BarberHub.Application.Repositories;
using BarberHub.Application.Security.Jwt;
using BarberHub.Application.Storage;
using BarberHub.Domain.Constants;
using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using File = BarberHub.Domain.Entities.File;

namespace BarberHub.Application.Services;

public class FileService(
    IFileRepository fileRepository,
    IFileStorageService fileStorageService,
    ICurrentUserService currentUserService)
{
    public async Task<FileDto> UploadFile(UploadFileDto uploadFileDto, CancellationToken cancellationToken = default)
    {
        ValidateSize(uploadFileDto.Size);
        ValidateContentType(uploadFileDto.ContentType);

        var storageKey = GenerateKey(uploadFileDto.OriginFileName);

        var uploadResult =
            await fileStorageService.UploadAsync(uploadFileDto.FileStream, storageKey, uploadFileDto.ContentType,
                cancellationToken);
        var file = new File(
            storageKey,
            uploadFileDto.OriginFileName,
            uploadResult.Url,
            uploadResult.Key,
            uploadFileDto.ContentType,
            uploadFileDto.Size,
            StorageProvider.ArvanCloud,
            currentUserService.CurrentUser.UserId);

        await fileRepository.AddAsync(file, cancellationToken);
        await fileRepository.SaveChangesAsync(cancellationToken);

        return ToDto(file);
    }

    public async Task<FileDto> GetByIdAsync(long fileId, CancellationToken cancellationToken = default)
    {
        var file = await fileRepository.GetByIdAsync(fileId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(File), fileId);
        return ToDto(file);
    }

    public async Task<FileDto> DeleteAsync(long fileId, CancellationToken cancellationToken = default)
    {
        var file = await fileRepository.GetByIdAsync(fileId, cancellationToken) ??
                   throw new EntityNotFoundException(nameof(File), fileId);
        
        await fileStorageService.DeleteAsync(file.StorageKey, cancellationToken);

        file.SoftDelete(currentUserService.CurrentUser.UserId);
        await fileRepository.SaveChangesAsync(cancellationToken);
        return ToDto(file);
    }

    private static void ValidateSize(long size)
    {
        switch (size)
        {
            case <= FileConstants.SizeMinLength:
                throw new InvalidFileSizeException();
            case > FileUploadConstants.MaxSizeBytes:
                throw new FileSizeLimitExceededException();
        }
    }

    private static void ValidateContentType(string contentType)
    {
        if (!FileUploadConstants.AllowedContentTypes.Contains(contentType))
            throw new UnsupportedFileTypeException();
    }

    private static string GenerateKey(string originFileName)
    {
        var extension = Path.GetExtension(originFileName);
        return $"{Guid.NewGuid()}{extension}";
    }

    private static FileDto ToDto(File file)
        => new
        (
            file.Id,
            file.FileName,
            file.OriginFileName,
            file.Url,
            file.ContentType,
            file.Size
        );
}