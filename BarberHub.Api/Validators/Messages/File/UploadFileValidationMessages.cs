namespace BarberHub.Api.Validators.Messages.File;

public static class UploadFileValidationMessages
{
    public const string FileProperty = "File";

    public const string OriginFileNameProperty = "OriginFileName";

    public const string ContentTypeUnsupported = "File type is not supported.";

    public const string SizeInvalid = "File size must be greater than zero.";
    public const string SizeExceeded = "File size exceeds the maximum allowed limit.";
}