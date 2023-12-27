using FluentValidation;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Resources;

namespace PublicTransportApi.Validators;

public class ScheduleEntryDTOValidator : AbstractValidator<ScheduleEntryDTO>
{
    public ScheduleEntryDTOValidator()
    {
        RuleFor(schedule => schedule.RecurringDays)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must(days => days!.All(c => char.IsDigit(c) || c.Equals(',')))
            .WithMessage(ErrorMessages.Schedule_RecurringDaysEmpty)
            .When(schedule => schedule.IsRecurring);

        RuleFor(schedule => schedule.DateTime).NotEmpty().WithMessage(ErrorMessages.Schedule_DateTimeEmpty);
    }
}