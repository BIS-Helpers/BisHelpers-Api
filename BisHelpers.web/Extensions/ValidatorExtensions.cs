using BisHelpers.Domain.Models;

namespace BisHelpers.web.Extensions;

public static class ValidatorExtensions
{
    public static IRuleBuilder<T, string> StringCustomValidator<T>(
        this IRuleBuilder<T, string> validator,
        bool NotNullOrEmpty = true,
        int? MaximumLength = null,
        (string pattern, string errorMessage)? regexPattern = null,
        List<string>? equalsToOne = null,
        bool isEmail = false)
    {
        if (NotNullOrEmpty)
            validator.NotEmpty()
                .WithMessage(Errors.RequiredField)
                .WithErrorCode("10");

        if (isEmail)
            validator.EmailAddress()
                .WithMessage(Errors.InvalidEmailAddress)
                .WithErrorCode("20");

        if (MaximumLength is not null)
            validator.MaximumLength((int)MaximumLength)
                .WithMessage(Errors.MaxLength)
                .WithErrorCode("30");

        if (regexPattern is not null)
            validator.Matches(regexPattern.Value.pattern)
                .WithMessage(regexPattern.Value.errorMessage)
                .WithErrorCode("40");

        if (equalsToOne is not null)
            validator.Must(x => equalsToOne.Select(w => w.ToUpper()).Contains(x.ToUpper()))
                .WithMessage(Errors.EqualsToOne(equalsToOne))
                .WithErrorCode("50");

        return validator;
    }

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
