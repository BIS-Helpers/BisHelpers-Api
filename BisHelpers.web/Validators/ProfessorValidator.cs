namespace BisHelpers.web.Validators;

public class ProfessorValidator : AbstractValidator<ProfessorCreateDto>
{
    public ProfessorValidator()
    {
        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200);
        RuleFor(r => r.FullName)
            .RegexCustomValidator(RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters)
            .Unless(x => string.IsNullOrEmpty(x.FullName));
    }
}
