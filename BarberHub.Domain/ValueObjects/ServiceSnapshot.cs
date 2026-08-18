using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.ValueObjects;

public abstract record ServiceSnapshot
{
    public string ServiceName { get; private set; } = null!;
    public TimeSpan ServiceDuration { get; private set; }
    public Money ServicePrice { get; private set; } = null!;

    private ServiceSnapshot()
    {
    }

    protected ServiceSnapshot(string serviceName, TimeSpan serviceDuration, Money servicePrice)
    {
        if (serviceDuration.Ticks <= 0) throw new InvalidServiceDurationSnapshotException();
        if (string.IsNullOrWhiteSpace(serviceName))
            throw new RequiredFieldException(serviceName);
        ServiceName = serviceName;
        ServiceDuration = serviceDuration;
        ServicePrice = servicePrice ?? throw new RequiredFieldException(serviceName);
    }
}