using BarberHub.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BarberHub.Infrastructure.Persistence.Mongo;

public class MongoContext
{
    private readonly MongoSetting _settings;

    public MongoContext(IOptions<MongoSetting> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        var database = client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoDatabase Database { get; }

    public IMongoCollection<ExceptionLog> ExceptionLogs =>
        Database.GetCollection<ExceptionLog>(_settings.ExceptionLogCollection);
}