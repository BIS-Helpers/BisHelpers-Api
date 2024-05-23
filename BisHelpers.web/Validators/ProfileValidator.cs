namespace BisHelpers.web.Validators;

public class ProfileValidator : AbstractValidator<ProfileUpdateDto>
{
    public ProfileValidator()
    {
        RuleFor(r => r.Email)
            .StringCustomValidator(MaximumLength: 128);

        RuleFor(r => r.Email)
            .EmailCustomValidator()
            .Unless(x => string.IsNullOrEmpty(x.Email));

        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200);
        RuleFor(r => r.FullName)
            .RegexCustomValidator(RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters)
            .Unless(x => string.IsNullOrEmpty(x.FullName));

        RuleFor(r => r.PhoneNumber)
            .StringCustomValidator(MaximumLength: 11);
        RuleFor(r => r.PhoneNumber)
            .RegexCustomValidator(RegexPatterns.MobileNumber, Errors.InvalidMobileNumber)
            .Unless(x => string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(r => r.Gender)
            .StringCustomValidator();
        RuleFor(r => r.Gender)
            .MatchCustomValidator(["Male", "Female"])
            .Unless(x => string.IsNullOrEmpty(x.Gender));
    }
}
