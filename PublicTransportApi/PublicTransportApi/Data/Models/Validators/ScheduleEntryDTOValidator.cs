using FluentValidation;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Resources;

namespace PublicTransportApi.Data.Models.Validators;

public class ScheduleEntryDTOValidator : AbstractValidator<ScheduleEntryDTO>
{
    public ScheduleEntryDTOValidator()
    {
        When(schedule => schedule.IsRecurring, () =>
        {
            RuleFor(schedule => schedule.RecurringDays)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(days => days!.All(c => char.IsDigit(c) || c.Equals(',')))
                .WithMessage(ErrorMessages.Schedule_RecurringDaysEmpty);

            RuleFor(schedule => schedule.RecurringDays)
                .NotEmpty()
                .Must(days => days!.Split(',').All(day => int.Parse(day) < 7))
                .WithMessage(ErrorMessages.Schedule_RecurringDaysEmpty)
                .When(schedule => schedule.RecurringDays!.Contains(','));

            RuleFor(schedule => schedule.RecurringDays)
                .NotEmpty()
                .Must(days => int.Parse(days!) < 7)
                .WithMessage(ErrorMessages.Schedule_RecurringDaysEmpty)
                .When(schedule => schedule.RecurringDays!.Length == 1 && char.IsDigit(schedule.RecurringDays[0]));
        });


        RuleFor(schedule => schedule.DateTime).NotEmpty().WithMessage(ErrorMessages.Schedule_DateTimeEmpty);
    }
}