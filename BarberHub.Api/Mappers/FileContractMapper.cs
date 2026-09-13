using BarberHub.Api.Constants.File;
using BarberHub.Api.Contracts.File;
using BarberHub.Application.DTOs.File;

namespace BarberHub.Api.Mappers;

public static class FileContractMapper
{
    public static FileResponse ToResponse(this FileDto fileDto)
        => new
        (
            fileDto.Id,
            fileDto.FileName,
            fileDto.OriginFileName,
            fileDto.Url,
            fileDto.ContentType,
            fileDto.Size
        );

    public static UploadFileDto ToDto(this UploadFileRequest request)
        => new(
            request.File.OpenReadStream(),
            request.OriginFileName,
            request.File.ContentType,
            request.File.Length
        );
}