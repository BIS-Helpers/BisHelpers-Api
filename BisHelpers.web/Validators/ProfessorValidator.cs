namespace BisHelpers.web.Validators;

public class ProfessorValidator : AbstractValidator<ProfessorCreateDto>
{
    public ProfessorValidator()
    {
        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200, regexPattern: (RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters));
    }
}
