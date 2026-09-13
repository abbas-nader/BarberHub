using Asp.Versioning;
using BarberHub.Api.Constants.File;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.File;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class FileController(FileService fileService) : BaseController
{
    [HttpPost(FileUriConstants.Upload)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<FileResponse>> Upload(UploadFileRequest uploadFileRequest,
        CancellationToken cancellationToken = default)
    {
        var file = await fileService.UploadAsync(uploadFileRequest.ToDto(), cancellationToken);
        return file.ToResponse();
    }

    [HttpDelete(FileUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<FileResponse>> Delete([FromRoute] long fileId,
        CancellationToken cancellationToken = default)
    {
        var  file = await fileService.DeleteAsync(fileId, cancellationToken);
        return file.ToResponse();
    }

    [HttpGet(FileUriConstants.GetById)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<FileResponse>> GetById([FromQuery] long fileId,
        CancellationToken cancellationToken = default)
    {
        var file = await fileService.GetByIdAsync(fileId, cancellationToken);
        return file.ToResponse();
    }
    
}