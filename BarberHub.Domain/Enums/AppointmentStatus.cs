namespace BarberHub.Domain.Enums;

public enum AppointmentStatus : byte
{
    Confirmed = 1,
    Completed = 2,
    CancelledByUser = 3,
    CancelledBySalon = 4,
    NoShow = 5
}