using BarberHub.Domain.Enums;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;
using BarberHub.Domain.ValueObjects;

namespace BarberHub.Domain.Entities;

public class Appointment : BaseEntity
{
    private readonly List<WalletTransaction> _walletTransactions = [];
    public DateOnly AppointmentDate { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public AppointmentStatus AppointmentStatus { get; private set; }
    public ServiceSnapshot ServiceSnapshot { get; private set; } = null!;
    public Money DepositAmountSnapshot { get; private set; } = null!;
    public DepositPaymentMethod DepositPaymentMethod { get; private set; }
    public DepositStatus DepositStatus { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }
    public NoShowDetectionType? NoShowDetectionType { get; private set; }

    public long BarberId { get; private set; }
    public long CustomerId { get; private set; }
    public long SalonId { get; private set; }
    public long BarberServiceId { get; private set; }

    public IReadOnlyCollection<WalletTransaction> WalletTransactions => _walletTransactions.AsReadOnly();

    private Appointment()
    {
    }

    public Appointment(DateOnly appointmentDate, TimeOnly startTime, TimeOnly endTime, ServiceSnapshot serviceSnapshot,
        Money depositAmountSnapshot, DepositPaymentMethod depositPaymentMethod, long barberId, long customerId,
        long salonId, long barberServiceId, long creationBy)
    {
        ValidateDate(appointmentDate);
        ValidateTimes(startTime, endTime);
        ValidateServiceSnapshot(serviceSnapshot);
        ValidateDepositAmountSnapshot(depositAmountSnapshot);
        AppointmentDate = appointmentDate;
        StartTime = startTime;
        EndTime = endTime;
        AppointmentStatus = AppointmentStatus.Confirmed;
        ServiceSnapshot = serviceSnapshot;
        DepositAmountSnapshot = depositAmountSnapshot;
        DepositPaymentMethod = depositPaymentMethod;
        DepositStatus = DepositStatus.Paid;
        CancelledAt = null;
        NoShowDetectionType = null;
        BarberId = barberId;
        CustomerId = customerId;
        SalonId = salonId;
        BarberServiceId = barberServiceId;
        Creation(creationBy);
    }

    public void Complete(long modifiedBy)
    {
        EnsureIsConfirmed();
        AppointmentStatus = AppointmentStatus.Completed;
        Modified(modifiedBy);
    }

    public void CancelByCustomer(long modifiedBy)
    {
        EnsureIsConfirmed();
        AppointmentStatus = AppointmentStatus.CancelledByCustomer;
        CancelledAt = DateTimeOffset.UtcNow;
        Modified(modifiedBy);
    }

    public void CancelBySalon(long modifiedBy)
    {
        EnsureIsConfirmed();
        AppointmentStatus = AppointmentStatus.CancelledBySalon;
        CancelledAt = DateTimeOffset.UtcNow;
        Modified(modifiedBy);
    }

    public void NoShow(NoShowDetectionType noShowDetectionType, long modifiedBy)
    {
        EnsureIsConfirmed();
        AppointmentStatus = AppointmentStatus.NoShow;
        NoShowDetectionType = noShowDetectionType;
        Modified(modifiedBy);
    }

    public void UpdateDepositStatus(DepositStatus depositStatus, long modifiedBy)
    {
        EnsureIsConfirmed();
        DepositStatus = depositStatus;
        Modified(modifiedBy);
    }

    private void EnsureIsConfirmed()
    {
        if (AppointmentStatus != AppointmentStatus.Confirmed)
            throw new InvalidAppointmentStatusTransitionException();
    }

    private static void ValidateDate(DateOnly appointmentDate)
    {
        if (appointmentDate < DateOnly.FromDateTime(DateTime.UtcNow))
            throw new InvalidAppointmentDateException();
    }

    private static void ValidateTimes(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime <= startTime)
            throw new InvalidAppointmentTimeRangeException();
    }

    private static void ValidateServiceSnapshot(ServiceSnapshot serviceSnapshot)
    {
        if (serviceSnapshot is null)
            throw new RequiredFieldException(nameof(serviceSnapshot));
    }

    private static void ValidateDepositAmountSnapshot(Money depositAmountSnapshot)
    {
        if (depositAmountSnapshot is null)
            throw new RequiredFieldException(nameof(depositAmountSnapshot));
    }
}