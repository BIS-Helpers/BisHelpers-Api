namespace BisHelpers.web.Validators;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
{
    public ResetPasswordValidator()
    {
        RuleFor(r => r.OldPassword)
            .StringCustomValidator();

        RuleFor(r => r.NewPassword)
            .StringCustomValidator(MaximumLength: 256);
        RuleFor(r => r.NewPassword)
            .RegexCustomValidator(RegexPatterns.Password, Errors.WeakPassword)
            .Unless(x => string.IsNullOrEmpty(x.NewPassword));
    }
}
