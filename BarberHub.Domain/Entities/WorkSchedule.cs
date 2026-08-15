using BarberHub.Domain.Exceptions;

namespace BarberHub.Domain.Entities;

public class WorkSchedule : BaseEntity
{
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public DayOfWeek DayOfWeek { get; private set; }

    public long BarberId { get; private set; }

    private WorkSchedule()
    {
    }

    public WorkSchedule(TimeOnly startTime, TimeOnly endTime, DayOfWeek dayOfWeek, long barberId, long creationBy)
    {
        StartTime = startTime;
        EndTime = endTime;
        DayOfWeek = dayOfWeek;
        BarberId = barberId;
        Creation(creationBy);
    }

    public void Update(TimeOnly startTime, TimeOnly endTime, DayOfWeek dayOfWeek, long modifiedBy)
    {
        StartTime = startTime;
        EndTime = endTime;
        DayOfWeek = dayOfWeek;
        Modified(modifiedBy);
    }
    public static void ValidateTimeRange(TimeOnly startTime, TimeOnly endTime)
    {
        if (endTime <= startTime)
            throw new InvalidWorkScheduleTimeRangeException();
    }
}