namespace BarberHub.Api.Constants.Gallery;

public static class GalleryUriConstants
{
    private const string ControllerName = "gallery";
    
    public const string GetAllBySalonId = $"{ControllerName}/salon/{{salonId}}";
    public const string GetById = $"{ControllerName}/{{galleryId}}";
    public const string Create = $"{ControllerName}/create";
    public const string UpdateCaption = $"{ControllerName}/update-caption/{{galleryId}}";
    public const string Delete = $"{ControllerName}/delete/{{galleryId}}";
}