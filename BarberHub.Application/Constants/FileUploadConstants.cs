namespace BarberHub.Application.Constants;

public static class FileUploadConstants
{
    public const long MaxSizeBytes = 5 * 1024 * 1024; 

    public static readonly HashSet<string> AllowedContentTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg",
        "image/png",
        "image/webp"
    };
}