namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Seed;

public class SeedAdminSetting
{
    public const string SectionName = "SeedAdmin";

    public string FirstName { get; set; } = "abbas";
    public string LastName { get; set; } = "nader";
    public string UserName { get; set; } =  "abbasnader";
    public string Password { get; set; } = string.Empty;
}