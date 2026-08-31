namespace BarberHub.Domain.Exceptions.SharedExceptions;

public class InvalidAuditUserIdException(string paramName) : Exception($"{paramName} must be a positive number.")
{
}