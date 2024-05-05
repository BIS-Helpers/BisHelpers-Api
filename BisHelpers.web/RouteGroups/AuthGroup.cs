namespace BisHelpers.web.RouteGroups;

public static class AuthGroup
{
    public static RouteGroupBuilder GroupAuth(this RouteGroupBuilder builder)
    {
        builder.MapPost("/register", async ([FromBody] RegisterDto dto, IValidator<RegisterDto> validator, IAuthService authService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var registerResponse = await authService.RegisterAsync(dto);

            if (!registerResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [registerResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Created();
        })
        .EndPointConfigurations("Register New User")
        .CreatedRouteConfiguration()
        .ErrorRouteConfiguration();

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
        .EndPointConfigurations("User Login")
        .OkRouteConfiguration<AuthDto>()
        .ErrorRouteConfiguration();

        builder.MapPost("/refreshToken", async (IAuthService authService, HttpContext context) =>
        {
            var refreshToken = context.Request.Cookies["refreshToken"] ?? string.Empty;

            var refreshTokenResult = await authService.RefreshTokenAsync(refreshToken);

            if (!refreshTokenResult.IsSuccess || refreshTokenResult.Model is null)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [refreshTokenResult.ErrorBody],
                    StatusCode = 400,
                });

            context.Response.SetRefreshTokenCookie(
                expiresOn: refreshTokenResult.Model.RefreshTokenExpiration,
                refreshToken: refreshTokenResult.Model.RefreshToken);

            return Results.Ok(refreshTokenResult.Model);
        })
        .EndPointConfigurations("Generate Refresh Token")
        .OkRouteConfiguration<AuthDto>()
        .ErrorRouteConfiguration();

        builder.MapPost("/resetPassword", [Authorize] async ([FromBody] ResetPasswordDto dto, IAuthService authService, IValidator<ResetPasswordDto> validator, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var resetPasswordResult = await authService.ResetPasswordAsync(dto, context.User.GetUserId());

            if (!resetPasswordResult.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [resetPasswordResult.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Ok("Password Reset Successfully");
        })
        .EndPointConfigurations("Reset User Password")
        .OkRouteConfiguration()
        .ErrorRouteConfiguration();

        builder.MapPut("/Profile", [Authorize] async ([FromBody] ProfileUpdateDto dto, IAuthService authService, IValidator<ProfileUpdateDto> validator, ClaimsPrincipal user, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });
            var updateProfileResult = await authService.UpdateProfileAsync(dto, user.GetUserId());

            if (!updateProfileResult.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [updateProfileResult.ErrorBody],
                    StatusCode = 400,
                });
            return Results.Ok("User Profile Updated Successfully");
        })
        .EndPointConfigurations("Update User Profile")
        .OkRouteConfiguration()
        .ErrorRouteConfiguration();

        builder.MapGet("/Profile", [Authorize] async (IAuthService authService, ClaimsPrincipal user, HttpContext context) =>
        {
            var resultResponse = await authService.GetProfileAsync(context.User.GetUserId());

            if (resultResponse.Model is null)
                return Results.NotFound();

            return Results.Ok(resultResponse.Model);
        })
        .EndPointConfigurations("Get User Profile")
        .OkRouteConfiguration<ProfileDto>()
        .ErrorRouteConfiguration();

        return builder;
    }
}
