using BarberHub.Application.Repositories;
using BarberHub.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public abstract class BaseRepository<TEntity>(BarberHubDbContext barberHubDbContext)
    : IRepository<TEntity> where TEntity : class
{
    protected readonly BarberHubDbContext BarberHubDbContext = barberHubDbContext;
    private readonly DbSet<TEntity> _dbSet = barberHubDbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task<PaginatedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var totalCount = _dbSet.Count();
        var items = await _dbSet.AsNoTracking()
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return new  PaginatedResult<TEntity>(items, pageNumber, pageSize, totalCount);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await _dbSet.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
        => _dbSet.Update(entity);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => BarberHubDbContext.SaveChangesAsync(cancellationToken);
}