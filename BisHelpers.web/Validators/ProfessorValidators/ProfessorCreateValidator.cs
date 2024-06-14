namespace BisHelpers.web.Validators.ProfessorValidators;

public class ProfessorCreateValidator : AbstractValidator<ProfessorCreateDto>
{
    public ProfessorCreateValidator()
    {
        RuleFor(r => r.FullName)
            .StringCustomValidator(MaximumLength: 200);
        RuleFor(r => r.FullName)
            .RegexCustomValidator(RegexPatterns.CharactersOnly_Eng, Errors.OnlyEnglishLetters)
            .Unless(x => string.IsNullOrEmpty(x.FullName));
    }
}
