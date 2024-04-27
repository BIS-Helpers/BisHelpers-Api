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
                .WithMessage(Errors.RequiredField);

        if (isEmail)
            validator.EmailAddress()
                .WithMessage(Errors.InvalidEmailAddress);

        if (MaximumLength is not null)
            validator.MaximumLength(128)
                .WithMessage(Errors.MaxLength);

        if (regexPattern is not null)
            validator.Matches(regexPattern.Value.pattern)
                .WithMessage(regexPattern.Value.errorMessage);

        if (equalsToOne is not null)
            validator.Must(x => equalsToOne.Select(w => w.ToUpper()).Contains(x.ToUpper()))
                .WithMessage(Errors.EqualsToOne(equalsToOne));

        return validator;
    }

    public static string ToCustomString(this ValidationResult validationResult)
    {
        var validationDictionary = validationResult.ToDictionary();
        var result = string.Join(", ", validationDictionary.Select(kvp => $"{kvp.Key}: {string.Join(", ", kvp.Value)}"));

        return result;
    }
}
