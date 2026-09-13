namespace BarberHub.Api.Validators.Messages.Gallery;

public static class CreateGalleryValidationMessages
{
    public const string FileProperty = "File";
    
    public const string OriginFileNameProperty = "OriginFileName";
    public const string CaptionProperty = "Caption";

    public const string BarberIdProperty = "BarberId";
    public const string BarberIdInvalid = "Barber id must be a positive number.";

    public const string ContentTypeUnsupported = "File type is not supported.";
    public const string SizeInvalid = "File size must be greater than zero.";
    public const string SizeExceeded = "File size exceeds the maximum allowed limit.";
}