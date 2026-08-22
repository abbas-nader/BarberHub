using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using MongoDB.Driver;

namespace BarberHub.Infrastructure.Persistence.Mongo.Repositories;

public class ExceptionLogRepository(MongoContext context) : IExceptionLogRepository
{
    private readonly IMongoCollection<ExceptionLog> _collection = context.ExceptionLogs;

    public Task AddAsync(ExceptionLog log, CancellationToken cancellationToken = default)
        => _collection.InsertOneAsync(log, options: null, cancellationToken);

    public async Task<ExceptionLog?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var exception = await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(cancellationToken);
        return exception;
    }

    public async Task<IReadOnlyList<ExceptionLog>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<ExceptionLog>.Filter.Empty)
            .SortByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ExceptionLog>> GetRecentAsync(int count,
        CancellationToken cancellationToken = default)
    {
        return await _collection.Find(Builders<ExceptionLog>.Filter.Empty)
            .SortByDescending(x => x.CreatedAt)
            .Limit(count)
            .ToListAsync(cancellationToken);
    }
}