namespace BarberHub.Api.Constants.Barber;

public static class BarberUriConstants
{
    private const string ControllerName = "Barber";
    public const string GetAllBySalonId = $"{ControllerName}/{{salonId}}";
    public const string GetById = $"{ControllerName}/{{barberId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update/{{barberId}}";
    public const string Delete = $"{ControllerName}/delete/{{barberId}}";
    public const string Activate = $"{ControllerName}/activate/{{barberId}}";
    public const string Deactivate = $"{ControllerName}/deactivate/{{barberId}}";
}