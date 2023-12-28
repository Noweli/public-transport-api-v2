using FluentValidation;
using PublicTransportApi.Resources;

namespace PublicTransportApi.Data.Models.Validators;

public class LineValidator : AbstractValidator<Line>
{
    public LineValidator()
    {
        RuleFor(line => line.Identifier).NotEmpty().WithMessage(ErrorMessages.Line_IdentifierCannotBeNull);
        RuleFor(line => line.Name).NotEmpty().WithMessage(ErrorMessages.Line_NameCannotBeNull);
    }
}