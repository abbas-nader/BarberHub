namespace BarberHub.Api.Constants.File;

public static class FileUriConstants
{
    private const string ControllerName = "file";

    public const string Upload = $"{ControllerName}/upload";
    public const string GetById = $"{ControllerName}/{{fileId}}";
    public const string Delete = $"{ControllerName}/delete/{{fileId}}";
}