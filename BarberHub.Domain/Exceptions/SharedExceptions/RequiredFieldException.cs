namespace BarberHub.Domain.Exceptions.SharedExceptions;

public class RequiredFieldException(string fieldName) : Exception($"{fieldName} cannot be null or empty.")
{
}