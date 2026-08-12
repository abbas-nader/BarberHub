using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.ValueObjects;

public sealed record ServiceSnapshot
{
    public string ServiceName { get; }
    public TimeSpan ServiceDuration { get; }
    public Money ServicePrice { get; }

    public ServiceSnapshot(string serviceName, TimeSpan serviceDuration, Money servicePrice)
    {
        if (serviceDuration.Ticks <= 0) throw new InvalidServiceDurationSnapshotException();
        if (string.IsNullOrWhiteSpace(serviceName))
            throw new RequiredFieldException(serviceName);
        ServiceName = serviceName;
        ServiceDuration = serviceDuration;
        ServicePrice = servicePrice ?? throw new RequiredFieldException(serviceName);
    }
}