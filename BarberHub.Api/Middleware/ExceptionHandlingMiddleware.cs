using System.Net;
using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;

namespace BarberHub.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, IExceptionLogRepository exceptionLogRepository)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var statusCode = ResolveStatusCode(ex);

            await LogExceptionAsync(context, ex, statusCode, exceptionLogRepository);
            await WriteProblemAsync(context, statusCode, ResolveDetail(ex, statusCode));
        }
    }

    private static HttpStatusCode ResolveStatusCode(Exception exception) => exception switch
    {
        EntityNotFoundException => HttpStatusCode.NotFound,

        InvalidCredentialsException
            or InvalidRefreshTokenException
            or RefreshTokenReuseDetectedException
            or RequiredClaimMissingException
            or UserNotAuthenticatedException => HttpStatusCode.Unauthorized,

        DuplicateUserNameException
            or EntityAlreadyDeletedException
            or ReviewAlreadyApprovedException
            or ReviewNotApprovedException
            or InvalidAppointmentStatusTransitionException
            or InsufficientMoneyException => HttpStatusCode.Conflict,

        RequiredFieldException
            or InvalidAuditUserIdException
            or InvalidMoneyAmountException
            or InvalidServiceDurationSnapshotException
            or InvalidRatingException
            or InvalidSalonDescriptionException
            or InvalidWorkScheduleTimeRangeException
            or InvalidFileSizeException
            or InvalidServiceDurationException
            or InvalidAppointmentDateException
            or InvalidAppointmentTimeRangeException
            or InvalidServiceDescriptionException
            or InvalidPageNumberException
            or InvalidPageSizeException
            or InvalidTotalCountException
            or CurrencyMismatchException => HttpStatusCode.BadRequest,

        _ => HttpStatusCode.InternalServerError
    };

    private static string ResolveDetail(Exception exception, HttpStatusCode statusCode) =>
        statusCode == HttpStatusCode.InternalServerError
            ? "An unexpected error occurred."
            : exception.Message;

    private async Task LogExceptionAsync(
        HttpContext context,
        Exception exception,
        HttpStatusCode statusCode,
        IExceptionLogRepository repository)
    {
        try
        {
            var log = ExceptionLog.CreateByException(
                exception,
                (int)statusCode,
                context.Request.Path,
                context.Request.Method);

            await repository.AddAsync(log, context.RequestAborted);
        }
        catch (Exception loggingException)
        {
            logger.LogError(loggingException,
                "Failed to persist exception log to MongoDB for {ExceptionType}",
                exception.GetType().Name);
        }
    }

    private static Task WriteProblemAsync(HttpContext context, HttpStatusCode statusCode, string detail)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = (int)statusCode;

        var problem = new
        {
            status = (int)statusCode,
            title = statusCode.ToString(),
            detail
        };

        return context.Response.WriteAsJsonAsync(problem);
    }
}