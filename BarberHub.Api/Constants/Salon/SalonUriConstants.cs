namespace BarberHub.Api.Constants.Salon;

public static class SalonUriConstants
{
    private const string ControllerName = "Salon";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{id}}";
    public const string Create = $"{ControllerName}/create";
    public const string Update = $"{ControllerName}/update";
    public const string Delete = $"{ControllerName}/delete";
    public const string Activate = $"{ControllerName}/activate";
    public const string Deactivate = $"{ControllerName}/deactivate";
    public const string UpdateDepositAmount = $"{ControllerName}/updateDepositAmount";
}