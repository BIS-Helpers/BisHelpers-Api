namespace BisHelpers.web.RouteGroups.VersionTwo;

public static class AuthGroup
{
    public static RouteGroupBuilder GroupAuthVersionTwo(this RouteGroupBuilder builder)
    {
        builder.MapPost("/login", async ([FromBody] LoginDto dto, IValidator<LoginDto> validator, IAuthService authService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var loginResult = await authService.GetTokenAsync(dto);

            if (!loginResult.IsSuccess || loginResult.Model is null)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [loginResult.ErrorBody],
                    StatusCode = 400,
                });

            context.Response.SetRefreshTokenCookie(
                expiresOn: loginResult.Model.RefreshTokenExpiration,
                refreshToken: loginResult.Model.RefreshToken);

            return Results.Ok(loginResult.Model);
        })
        .EndPointConfigurations("User Login", Versions.Version2)
        .OkResponseConfiguration<AuthDto>()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest);

        return builder;
    }
}
