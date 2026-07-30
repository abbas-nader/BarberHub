namespace BarberHub.Domain.Exceptions;

public class InvalidServiceNameSnapshotException()
    : Exception("Invalid service name snapshot. Service name cannot be null or empty.")
{
}