namespace BisHelpers.web.Validators;

public class ProfileValidator : AbstractValidator<ProfileUpdateDto>
{
    public ProfileValidator()
    {
        RuleFor(r => r.Email)
            .StringCustomValidator(MaximumLength: 128, isEmail: true);
        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200, regexPattern: (RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters));
        RuleFor(r => r.PhoneNumber)
            .StringCustomValidator(MaximumLength: 11, regexPattern: (RegexPatterns.MobileNumber, Errors.InvalidMobileNumber));
        RuleFor(r => r.Gender)
            .StringCustomValidator(equalsToOne: ["Male", "Female"]);
    }
}
