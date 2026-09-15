using Asp.Versioning;
using BarberHub.Api.Constants.Gallery;
using BarberHub.Api.Contracts;
using BarberHub.Api.Contracts.Gallery;
using BarberHub.Api.Mappers;
using BarberHub.Application.Services.InterFaces;
using BarberHub.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers.V1;

[ApiVersion("1.0")]
public class GalleryController(IGalleryService galleryService) : BaseController
{
    [HttpGet(GalleryUriConstants.GetAllBySalonId)]
    [AllowAnonymous]
    public async Task<ApiResult<IReadOnlyList<GalleryResponse>>> GetAllBySalonId([FromRoute] long salonId,
        CancellationToken cancellationToken = default)
    {
        var galleries = await galleryService.GetAllBySalonIdAsync(salonId, cancellationToken);
        return galleries.Select(x => x.ToResponse()).ToList();
    }

    [HttpGet(GalleryUriConstants.GetById)]
    [AllowAnonymous]
    public async Task<ApiResult<GalleryResponse>> GetById([FromRoute] long salonId,
        CancellationToken cancellationToken = default)
    {
        var galleries = await galleryService.GetByIdAsync(salonId, cancellationToken);
        return galleries.ToResponse();
    }

    [HttpPost(GalleryUriConstants.Create)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<GalleryResponse>> Create([FromForm] CreateGalleryRequest request,
        CancellationToken cancellationToken = default)
    {
        var gallery = await galleryService.CreateAsync(request.ToDto(), cancellationToken);
        return gallery.ToResponse();
    }

    [HttpPatch(GalleryUriConstants.UpdateCaption)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<GalleryResponse>> Update([FromRoute] long galleryId,
        [FromBody] UpdateGalleyCaptionRequest request,
        CancellationToken cancellationToken = default)
    {
        var gallery = await galleryService.UpdateAsync(galleryId, request.ToDto(), cancellationToken);
        return gallery.ToResponse();
    }

    [HttpPatch(GalleryUriConstants.Delete)]
    [Authorize(Roles = nameof(UserRole.SalonAdmin))]
    public async Task<ApiResult<GalleryResponse>> Delete([FromRoute] long galleryId,
        CancellationToken cancellationToken = default)
    {
        var gallery = await galleryService.DeleteAsync(galleryId, cancellationToken);
        return gallery.ToResponse();
    }
}