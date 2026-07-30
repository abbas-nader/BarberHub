namespace BarberHub.Domain.Exceptions;

public class InvalidServicePriceSnapshotException() : Exception("Invalid service price snapshot. Price cannot be null.")
{
}