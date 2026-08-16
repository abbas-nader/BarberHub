using BarberHub.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public abstract class BaseRepository<TEntity>(BarberHubDbContext barberHubDbContext) : IRepository<TEntity> where TEntity : class
{
    protected readonly BarberHubDbContext BarberHubDbContext = barberHubDbContext;
    private readonly DbSet<TEntity> _dbSet =  barberHubDbContext.Set<TEntity>();

    public async Task<TEntity?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
        => await _dbSet.FindAsync([id], cancellationToken);

    public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    => await _dbSet.AsNoTracking().ToListAsync(cancellationToken);

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    => await  _dbSet.AddAsync(entity, cancellationToken);

    public void Update(TEntity entity)
    => _dbSet.Update(entity);

    public void DeleteAsync(TEntity entity)
    => _dbSet.Remove(entity);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    => BarberHubDbContext.SaveChangesAsync(cancellationToken);
}