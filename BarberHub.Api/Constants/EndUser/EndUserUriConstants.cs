namespace BarberHub.Api.Constants.EndUser;

public static class EndUserUriConstants
{
    private const string ControllerName = "end-user";
    public const string GetAll = $"{ControllerName}";
    public const string GetById = $"{ControllerName}/{{endUserId}}";
    public const string Update = $"{ControllerName}/update";
    public const string Delete = $"{ControllerName}/delete/{{endUserId}}";
}