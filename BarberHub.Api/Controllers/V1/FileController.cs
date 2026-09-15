using Asp.Versioning;
using BarberHub.Api.Constants.File;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.File;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class FileController(IFileService fileService) : BaseController
{
    [HttpGet(FileUriConstants.GetById)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<FileResponse>> GetById([FromRoute] long fileId,
        CancellationToken cancellationToken = default)
    {
        var file = await fileService.GetByIdAsync(fileId, cancellationToken);
        return file.ToResponse();
    }
}