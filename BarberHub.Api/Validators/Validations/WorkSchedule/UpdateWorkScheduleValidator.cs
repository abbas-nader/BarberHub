using BarberHub.Api.Contracts.WorkSchedule;
using BarberHub.Api.Validators.Messages.WorkSchedule;
using FluentValidation;

namespace BarberHub.Api.Validators.Validations.WorkSchedule;

public class UpdateWorkScheduleValidator : AbstractValidator<UpdateWorkScheduleRequest>
{
    public UpdateWorkScheduleValidator()
    {
        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .WithMessage(UpdateWorkScheduleValidationMessages.DayOfWeekInvalid);

        RuleFor(x => x.EndTime)
            .GreaterThan(x => x.StartTime)
            .WithMessage(UpdateWorkScheduleValidationMessages.TimeRangeInvalid);
    }
}