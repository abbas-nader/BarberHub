namespace BarberHub.Domain.Exceptions;

public class InvalidSalonAdminFullNameException()
    : Exception("Invalid salon admin full name. Full name exceeds the maximum allowed length.")
{
}