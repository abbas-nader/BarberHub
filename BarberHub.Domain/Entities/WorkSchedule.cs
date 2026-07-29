namespace BarberHub.Domain.Entities;

public class WorkSchedule : BaseEntity
{
    public TimeOnly StartTime { get;private set; }
    public TimeOnly EndTime { get;private set; }
    public DayOfWeek DayOfWeek { get;private set; }
    
    public long BarberId { get;private set; }
    public Barber Barber { get;private set; } = null!;
}
