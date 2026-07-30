namespace BarberHub.Domain.Enums;

public enum AppointmentStatus : byte
{
    Confirmed = 1,
    Completed = 2,
    CancelledByCustomer = 3,
    CancelledBySalon = 4,
    NoShow = 5
}