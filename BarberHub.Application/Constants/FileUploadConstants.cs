namespace BarberHub.Application.Constants;

public static class FileUploadConstants
{
    public const long MaxSizeBytes = 5 * 1024 * 1024; 

    public static readonly IReadOnlyList<string> AllowedContentTypes =
    [
        "image/jpeg",
        "image/png",
        "image/webp"
    ];
}