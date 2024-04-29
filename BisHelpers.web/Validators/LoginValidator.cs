namespace BisHelpers.web.Validators;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(r => r.Email)
            .StringCustomValidator(MaximumLength: 128, isEmail: true);

        RuleFor(r => r.Password)
            .StringCustomValidator();
    }
}
