using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Domain.Entities;

public class EndUser : BaseEntity
{
    private readonly List<Appointment> _appointments = [];
    private readonly List<WalletTransaction> _walletTransactions = [];

    public long UserId { get; private set; }
    public User User { get; private set; } = null!;

    public IReadOnlyCollection<Appointment> Appointments => _appointments.AsReadOnly();
    public IReadOnlyCollection<WalletTransaction> WalletTransactions => _walletTransactions.AsReadOnly();

    private EndUser()
    {
    }

    public EndUser(long userId, long creationBy)
    {
        UserId = userId;
        Creation(creationBy);
    }
}