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
                return Results.BadRequest(ResponseErrors.Register(details: registerResult.ErrorMessage));

            return Results.Ok("User Registered Successfully");
        });

        builder.MapPost("login", async ([FromBody] LoginDto loginForm, IValidator<LoginDto> validator, IAuthService authService, HttpResponse response) =>
        {
            var validationResult = validator.Validate(loginForm);

            if (!validationResult.IsValid)
                return Results.BadRequest(ResponseErrors.Validation(details: validationResult.ToCustomString()));

            var loginResult = await authService.GetTokenAsync(loginForm);

            if (!loginResult.IsSuccess || loginResult.model is null)
                return Results.BadRequest(ResponseErrors.Login(details: loginResult.ErrorMessage));

            response.SetRefreshTokenCookie(
                expiresOn: loginResult.model.RefreshTokenExpiration,
                refreshToken: loginResult.model.RefreshToken);

            return Results.Ok(loginResult.model);
        });

        builder.MapPost("refreshToken", async (IAuthService authService, HttpRequest request, HttpResponse response) =>
        {
            var refreshToken = request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
                return Results.BadRequest(ResponseErrors.RefreshToken(details: "Request Cookie: \"refreshToken\" is empty!"));

            var refreshTokenResult = await authService.RefreshTokenAsync(refreshToken);

            if (!refreshTokenResult.IsSuccess || refreshTokenResult.model is null)
                return Results.BadRequest(ResponseErrors.RefreshToken(details: refreshTokenResult.ErrorMessage));

            response.SetRefreshTokenCookie(
                expiresOn: refreshTokenResult.model.RefreshTokenExpiration,
                refreshToken: refreshTokenResult.model.RefreshToken);

            return Results.Ok(refreshTokenResult.model);
        });

        builder.MapGet("getProfile", [Authorize] async (IAuthService authService, ClaimsPrincipal user) =>
        {
            var getProfileResult = await authService.GetProfileAsync(user.GetUserId());

            if (!getProfileResult.IsSuccess)
                return Results.BadRequest(ResponseErrors.Get(details: getProfileResult.ErrorMessage));

            return Results.Ok(getProfileResult.model);
        });

        return builder;
    }
}
