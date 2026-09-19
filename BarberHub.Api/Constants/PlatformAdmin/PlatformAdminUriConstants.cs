namespace BarberHub.Api.Constants.PlatformAdmin;

public static class PlatformAdminUriConstants
{
    private const string ControllerName = "platform-admin";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{platformAdminId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update";
    public const string Delete = $"{ControllerName}/delete/{{platformAdminId}}";
}