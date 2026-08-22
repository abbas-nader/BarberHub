using BarberHub.Api.Contracts.ExceptionLog;
using BarberHub.Application.DTOs;

namespace BarberHub.Api.Mappers;

public static class ExceptionLogContractMappings
{
    public static ExceptionLogResponse ToResponse(this ExceptionLogDto exceptionLogDto)
        => new(
            exceptionLogDto.Id,
            exceptionLogDto.ExceptionType,
            exceptionLogDto.Message,
            exceptionLogDto.StackTrace,
            exceptionLogDto.Source,
            exceptionLogDto.RequestPath,
            exceptionLogDto.RequestMethod,
            exceptionLogDto.StatusCode,
            exceptionLogDto.CreatedAt
        );
}