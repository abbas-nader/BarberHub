namespace BarberHub.Domain.Exceptions;

public class InvalidServiceDurationSnapshotException()
    : Exception("Invalid service duration snapshot. Duration must be greater than zero.")
{
}