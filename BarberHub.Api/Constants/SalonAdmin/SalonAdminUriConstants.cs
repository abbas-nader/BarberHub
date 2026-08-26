namespace BarberHub.Api.Constants.SalonAdmin;

public static class SalonAdminUriConstants
{
    private const string ControllerName = "SalonAdmin";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{id}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update";
    public const string Delete = $"{ControllerName}/delete";
}