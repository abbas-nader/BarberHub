using BarberHub.Domain.Enums;

namespace BarberHub.Application.Storage;

public interface IArvanAvailabilityChecker
{
    Task<bool> IsAvailableAsync(CancellationToken cancellationToken = default);
}