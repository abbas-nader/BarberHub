namespace BarberHub.Api.Validators.Messages.File;

public static class UploadFileValidationMessages
{
    public const string FileStreamProperty = "FileStream";

    public const string OriginFileNameProperty = "OriginFileName";

    public const string ContentTypeProperty = "ContentType";
    public const string ContentTypeUnsupported = "File type is not supported.";

    public const string SizeProperty = "Size";
    public const string SizeInvalid = "File size must be greater than zero.";
    public const string SizeExceeded = "File size exceeds the maximum allowed limit.";
}