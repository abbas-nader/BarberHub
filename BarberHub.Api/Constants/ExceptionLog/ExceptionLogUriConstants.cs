namespace BarberHub.Api.Constants.ExceptionLog;

public static class ExceptionLogUriConstants
{
    private const string ControllerName = "exceptionlog";

    public const string GetAll = $"{ControllerName}";
    public const string GetRecent = $"{ControllerName}/recent";
    public const string GetById = $"{ControllerName}/{{id}}";
}