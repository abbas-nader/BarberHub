using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class CustomerRepository(BarberHubDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
{
    public async Task<Customer?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default)
        => await BarberHubDbContext.Customers.FirstOrDefaultAsync(x => x.UserName == userName && x.IsDeleted == false,
            cancellationToken);
}