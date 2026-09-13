using BarberHub.Api.Contracts.Gallery;
using BarberHub.Application.DTOs.Gallery;

namespace BarberHub.Api.Mappers;

public static class GalleryContractMapper
{
    public static GalleryResponse ToResponse(this GalleryDto dto)
        => new(
            dto.Id,
            dto.Caption,
            dto.SalonId,
            dto.BarberId,
            dto.FileId
        );

    public static CreateGalleryDto ToDto(this CreateGalleryRequest request)
        => new(
            request.File.OpenReadStream(),
            request.OriginFileName,
            request.File.ContentType,
            request.File.Length,
            request.Caption,
            request.BarberId
        );

    public static UpdateGalleryCaptionDto ToDto(this UpdateGalleyCaptionRequest request)
        => new(
            request.Caption
        );
}