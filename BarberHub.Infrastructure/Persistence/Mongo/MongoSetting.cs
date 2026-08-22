namespace BarberHub.Infrastructure.Persistence.Mongo;

public class MongoSetting
{
    public const string SectionName = "Mongo";
    
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "BarberHubDb";
    public string ExceptionLogCollection { get; set; } = "exception_logs";
}