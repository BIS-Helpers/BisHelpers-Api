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
            .StringCustomValidator(MaximumLength: 200);
        RuleFor(r => r.FullName)
            .RegexCustomValidator(RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters)
            .Unless(x => string.IsNullOrEmpty(x.FullName));

        RuleFor(r => r.Password)
            .StringCustomValidator(MaximumLength: 256);
        RuleFor(r => r.Password)
            .RegexCustomValidator(RegexPatterns.Password, Errors.WeakPassword)
            .Unless(x => string.IsNullOrEmpty(x.Password));

        RuleFor(r => r.CollegeId)
            .StringCustomValidator(MaximumLength: 9);
        RuleFor(r => r.CollegeId)
            .RegexCustomValidator(RegexPatterns.CollageId, Errors.InvalidCollegeId)
            .Unless(x => string.IsNullOrEmpty(x.CollegeId));

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
