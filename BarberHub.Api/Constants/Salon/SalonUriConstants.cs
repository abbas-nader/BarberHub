namespace BarberHub.Api.Constants.Salon;

public static class SalonUriConstants
{
    private const string ControllerName = "Salon";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{salonId}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update";
    public const string Delete = $"{ControllerName}/delete/{{salonId}}";
    public const string Activate = $"{ControllerName}/activate/{{salonId}}";
    public const string Deactivate = $"{ControllerName}/deactivate/{{salonId}}";
    public const string UpdateDepositAmount = $"{ControllerName}/updateDepositAmount";
}