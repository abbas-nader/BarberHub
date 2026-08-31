using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public abstract class BaseRepository<TEntity>(BarberHubDbContext barberHubDbContext)
    : IRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly BarberHubDbContext BarberHubDbContext = barberHubDbContext;
    private readonly DbSet<TEntity> _dbSet = barberHubDbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await _dbSet.FirstOrDefaultAsync(x=> x.Id == id && x.IsDeleted == false, cancellationToken);

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _dbSet.AsNoTracking()
            .Where(x => x.IsDeleted == false)
            .ToListAsync(cancellationToken);

    public async Task<PaginatedResult<TEntity>> GetPagedAsync(int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = _dbSet.Where(x => x.IsDeleted == false);
        var totalCount =await _dbSet.CountAsync(cancellationToken);
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