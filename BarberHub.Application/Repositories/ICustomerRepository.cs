using BarberHub.Domain.Entities;

namespace BarberHub.Application.Repositories;

public interface ICustomerRepository : IRepository<Customer>
{
    Task<Customer?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}