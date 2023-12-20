using FluentValidation.Results;

namespace PublicTransportApi.Helpers.Extensions;

public static class StringExtensions
{
    public static string GetConcatenatedString(this IEnumerable<string> input, char joinCharacter = ' ') =>
        string.Join(joinCharacter, input);

    public static string GetConcatenatedErrorMessages(this IEnumerable<ValidationFailure> input,
        char joinCharacter = ' ') => input.Select(error => error.ErrorMessage).GetConcatenatedString();
}