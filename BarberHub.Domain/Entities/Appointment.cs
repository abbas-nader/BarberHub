using BarberHub.Domain.Enums;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Appointment : BaseEntity
{
    public DateOnly AppointmentDate {get; private set; }
    public TimeOnly StartTime {get; private set;}
    public TimeOnly EndTime {get; private set;}
    public AppointmentStatus AppointmentStatus { get; private set; } 
    public ServiceSnapshot ServiceSnapshot { get; private set; } = null!;
    public Money DepositAmountSnapshot { get; private set; } = null!;
    public DepositPaymentMethod DepositPaymentMethod { get; private set; }
    public DepositStatus DepositStatus { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }
    public NoShowDetectionType?  NoShowDetectionType { get; private set; }
    
}