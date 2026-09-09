using BarberHub.Api.Contracts.WorkSchedule;
using BarberHub.Api.Validators.Messages.WorkSchedule;
using BarberHub.Domain.Constants;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.WorkSchedule;

public class CreateWorkScheduleValidator : AbstractValidator<CreateWorkScheduleRequest>
{
    public CreateWorkScheduleValidator()
    {
        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .WithMessage(CreateWorkScheduleValidationMessages.DayOfWeekInvalid);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(CreateWorkScheduleValidationMessages.TimeRangeInvalid);

        RuleFor(x => x.BarberId)
            .GreaterThan(WorkScheduleConstants.BarberIdMinValue)
            .WithMessage(CreateWorkScheduleValidationMessages.BarberIdInvalid);
    }
}