using BisHelpers.Domain.Models;

namespace BisHelpers.web.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilder<T, string> StringCustomValidator<T>(this IRuleBuilder<T, string> validator, bool NotNullOrEmpty = true, int? MaximumLength = null)
    {
        if (NotNullOrEmpty)
            validator.NotEmpty()
                .WithMessage(Errors.RequiredField)
                .WithErrorCode("10");

        if (MaximumLength is not null)
            validator.MaximumLength((int)MaximumLength)
                .WithMessage(Errors.MaxLength)
                .WithErrorCode("30");

        return validator;
    }

    public static IRuleBuilderOptions<T, string> MatchCustomValidator<T>(this IRuleBuilder<T, string> validator, List<string> equalsToOne) =>
        validator.Must(x => equalsToOne.Select(w => w.ToUpper()).Contains(x.ToUpper()))
            .WithMessage(Errors.EqualsToOne(equalsToOne))
            .WithErrorCode("50");

    public static IRuleBuilderOptions<T, string> EmailCustomValidator<T>(this IRuleBuilder<T, string> validator) =>
        validator.EmailAddress()
            .WithMessage(Errors.InvalidEmailAddress)
            .WithErrorCode("20");

    public static IRuleBuilderOptions<T, string> RegexCustomValidator<T>(this IRuleBuilder<T, string> validator, string pattern, string errorMessage) =>
        validator.Matches(pattern)
            .WithMessage(errorMessage)
            .WithErrorCode("40");

    public static IEnumerable<ErrorBody?> ToErrorList(this ValidationResult validationResult)
    {
        var validationErrors = validationResult.Errors
            .GroupBy(x => x.PropertyName)
            .Select(g => new ErrorBody
            {
                Message = g.Key,
                Details = g.Select(v => $"{v.ErrorMessage}"),
                Code = string.Join(',', g.Select(v => v.ErrorCode).Distinct()),
                Suggestion = "Please review the request and the documentation https://bishelpers.apidog.io/"
            }).ToList();

        return validationErrors;
    }
}
