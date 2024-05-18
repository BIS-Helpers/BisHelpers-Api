namespace BisHelpers.web.Validators;

public class RegisterValidator : AbstractValidator<RegisterDto>
{
    public RegisterValidator()
    {
        RuleFor(r => r.Email)
            .StringCustomValidator(MaximumLength: 128);

        RuleFor(r => r.Email)
            .EmailCustomValidator()
            .Unless(x => string.IsNullOrEmpty(x.Email));

        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200, regexPattern: (RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters));
        RuleFor(r => r.Password)
            .StringCustomValidator(MaximumLength: 256, regexPattern: (RegexPatterns.Password, Errors.WeakPassword));
        RuleFor(r => r.CollegeId)
            .StringCustomValidator(MaximumLength: 9, regexPattern: (RegexPatterns.CollageId, Errors.InvalidCollegeId));
        RuleFor(r => r.PhoneNumber)
            .StringCustomValidator(MaximumLength: 11, regexPattern: (RegexPatterns.MobileNumber, Errors.InvalidMobileNumber));
        RuleFor(r => r.Gender)
            .StringCustomValidator(equalsToOne: ["Male", "Female"]);
    }
}
