using BarberHub.Domain.Exceptions;

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
            throw new InvalidServiceNameSnapshotException();
        ServiceName = serviceName;
        ServiceDuration = serviceDuration;
        ServicePrice = servicePrice ?? throw new InvalidServicePriceSnapshotException();
    }
}