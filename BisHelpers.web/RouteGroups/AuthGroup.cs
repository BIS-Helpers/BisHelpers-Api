namespace BisHelpers.web.RouteGroups;

public static class AuthGroup
{
    public static RouteGroupBuilder GroupAuth(this RouteGroupBuilder builder)
    {
        builder.MapPost("register", async ([FromBody] RegisterDto registerForm, IValidator<RegisterDto> validator, IAuthService authService) =>
        {
            var validationResult = validator.Validate(registerForm);

            if (!validationResult.IsValid)
                return Results.BadRequest(ResponseErrors.Validation(details: validationResult.ToCustomString()));

            var registerResult = await authService.RegisterAsync(registerForm);

            if (!registerResult.IsSuccess)
                return Results.BadRequest(ResponseErrors.UnableToRegister(details: registerResult.ErrorMessage));

            return Results.Ok("User Registered Successfully");
        });

        return builder;
    }
}
