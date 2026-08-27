namespace BarberHub.Api.Constants.SalonAdmin;

public static class SalonAdminUriConstants
{
    private const string ControllerName = "SalonAdmin";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{salonAdminId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update/{{salonAdminId}}";
    public const string Delete = $"{ControllerName}/delete/{{salonAdminId}}";
}