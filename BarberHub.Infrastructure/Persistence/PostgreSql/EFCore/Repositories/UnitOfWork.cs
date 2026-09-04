using System.Data;
using BarberHub.Application.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class UnitOfWork(BarberHubDbContext context) : IUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task BeginTransaction(CancellationToken cancellationToken = default)
    {
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransaction(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;
        try
        {
            await _transaction.CommitAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransaction(CancellationToken cancellationToken = default)
    {
        if (_transaction is null) return;
        try
        {
            await _transaction.RollbackAsync(cancellationToken);
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}