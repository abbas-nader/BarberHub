using BarberHub.Api.Contracts.WorkSchedule;
using BarberHub.Application.DTOs.WorkSchedule;
using BarberHub.Domain.Entities;

namespace BarberHub.Api.Mappers;

public static class WorkScheduleMapper
{
    public static WorkScheduleResponse ToResponse(this WorkScheduleDto workScheduleDto)
        => new(
            workScheduleDto.Id,
            workScheduleDto.StartTime,
            workScheduleDto.EndTime,
            workScheduleDto.DayOfWeek,
            workScheduleDto.BarberId
        );

    public static CreateWorkScheduleDto ToDto(this CreateWorkScheduleRequest request)
        => new(
            request.StartTime,
            request.EndTime,
            request.DayOfWeek,
            request.BarberId
        );

    public static UpdateWorkScheduleDto ToDto(this UpdateWorkScheduleRequest request)
        => new(
            request.StartTime,
            request.EndTime,
            request.DayOfWeek
        );
}