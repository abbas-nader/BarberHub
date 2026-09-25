using System.Net;
using BarberHub.Application.Repositories;
using BarberHub.Domain.Entities;
using BarberHub.Domain.Exceptions;
using BarberHub.Domain.Exceptions.SharedExceptions;
using Microsoft.EntityFrameworkCore;

namespace BarberHub.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context, IExceptionLogRepository exceptionLogRepository)
    {
        try
        {
            await next(context);
        }
        catch (OperationCanceledException) when (context.RequestAborted.IsCancellationRequested)
        {
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
        RequiredClaimMissingException => HttpStatusCode.Forbidden,
        InvalidCredentialsException
            or InvalidRefreshTokenException
            or RefreshTokenReuseDetectedException
            or InvalidCurrentPasswordException
            or UserNotAuthenticatedException => HttpStatusCode.Unauthorized,

        DuplicateUserNameException
            or EntityAlreadyDeletedException
            or ReviewAlreadyApprovedException
            or ReviewNotApprovedException
            or InvalidAppointmentStatusTransitionException
            or CancellationWindowExpiredException
            or CannotDeleteOwnAccountException
            or MobileNumberAlreadyVerifiedException
            or DbUpdateConcurrencyException
            or InvalidOtpStatusTransitionException
            or DbUpdateException { InnerException: Npgsql.PostgresException { SqlState: Npgsql.PostgresErrorCodes.UniqueViolation } }
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
            or FileSizeLimitExceededException
            or UnsupportedFileTypeException
            or InvalidBarberDescriptionException
            or InvalidOtpCodeException
            or OtpExpiredException
            or CurrencyMismatchException => HttpStatusCode.BadRequest,

        OtpRateLimitExceededException => HttpStatusCode.TooManyRequests,

        SmsSendFailedException => HttpStatusCode.BadGateway,
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

            await repository.AddAsync(log, CancellationToken.None);
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