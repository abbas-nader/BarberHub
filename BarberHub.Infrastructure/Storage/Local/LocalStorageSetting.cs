namespace BarberHub.Infrastructure.Storage.Local;

public class LocalStorageSetting
{
    public const string SectionName = "LocalStorage";

    public string RootPath { get; set; } = "wwwroot/uploads";
    public string PublicUrl { get; set; } = "/uploads";
}