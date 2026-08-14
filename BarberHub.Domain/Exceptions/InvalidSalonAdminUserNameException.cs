namespace BarberHub.Domain.Exceptions;

public class InvalidSalonAdminUserNameException()
    : Exception("Invalid salon admin username. Username exceeds the maximum allowed length.")
{
}