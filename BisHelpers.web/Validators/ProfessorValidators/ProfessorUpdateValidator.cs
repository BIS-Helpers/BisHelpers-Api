namespace BisHelpers.web.Validators.ProfessorValidators;

public class ProfessorUpdateValidator : AbstractValidator<ProfessorUpdateDto>
{
    public ProfessorUpdateValidator()
    {
        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200);
        RuleFor(r => r.FullName)
            .RegexCustomValidator(RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters)
            .Unless(x => string.IsNullOrEmpty(x.FullName));
    }
}
