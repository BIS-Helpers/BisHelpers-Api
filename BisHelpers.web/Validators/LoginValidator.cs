namespace BisHelpers.web.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(r => r.Email)
            .StringCustomValidator(MaximumLength: 128);

        RuleFor(r => r.Email)
            .EmailCustomValidator()
            .Unless(x => string.IsNullOrEmpty(x.Email));

        RuleFor(r => r.Password)
            .StringCustomValidator();
    }
}
