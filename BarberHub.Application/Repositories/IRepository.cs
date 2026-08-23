using BarberHub.Domain.Entities;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<PaginatedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default);

    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    void Update(TEntity entity);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}