using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;

namespace BarberHub.Infrastructure.Persistence.PostgreSql.EFCore.Repositories;

public class CustomerRepository(BarberHubDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
{
}