namespace BarberHub.Api.Constants.Service;

public class ServiceUriConstants
{
    private const string ControllerName = "Service";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{serviceId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update/{{serviceId}}";
    public const string Delete = $"{ControllerName}/delete/{{serviceId}}";
}