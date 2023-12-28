using FluentValidation;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Resources;

namespace PublicTransportApi.Data.Models.Validators;

public class StopPointDTOValidator : AbstractValidator<StopPointDTO>
{
    public StopPointDTOValidator()
    {
        RuleFor(stop => stop.Identifier)
            .NotEmpty()
            .WithMessage(ErrorMessages.StopPoint_IdentifierEmpty);

        RuleFor(stop => stop.Name)
            .NotEmpty()
            .WithMessage(ErrorMessages.StopPoint_NameEmpty);

        When(stop => string.IsNullOrEmpty(stop.StreetName), () =>
        {
            RuleFor(stop => stop.Lat)
                .NotEmpty()
                .WithMessage(ErrorMessages.StopPoint_LatLongEmpty);

            RuleFor(stop => stop.Long)
                .NotEmpty()
                .WithMessage(ErrorMessages.StopPoint_LatLongEmpty);
        });
    }
}