namespace BarberHub.Api.Constants.BarberService;

public static class BarberServiceUriConstants
{
    private const string ControllerName = "barber-service";
    public const string GetAllByBarberId = $"{ControllerName}/barber/{{barberId}}";
    public const string GetById = $"{ControllerName}/{{serviceId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update/{{serviceId}}";
    public const string Delete = $"{ControllerName}/delete/{{serviceId}}";
}