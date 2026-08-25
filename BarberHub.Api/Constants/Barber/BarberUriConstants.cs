namespace BarberHub.Api.Constants.Barber;

public static class BarberUriConstants
{
    private const string ControllerName = "Barber";
    public const string GetAllBySalonId = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{id}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update";
    public const string Delete = $"{ControllerName}/delete";
    public const string Activate = $"{ControllerName}/activate";
    public const string Deactivate = $"{ControllerName}/deactivate";
}