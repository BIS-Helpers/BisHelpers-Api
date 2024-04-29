namespace BisHelpers.web.RouteGroups;

public static class AuthGroup
{
    public static RouteGroupBuilder GroupAuth(this RouteGroupBuilder builder)
    {
        builder.MapPost("/register", async ([FromBody] RegisterDto dto, IValidator<RegisterDto> validator, IAuthService authService) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(ResponseErrors.Validation(details: validationResult.ToCustomString()));

            var registerResult = await authService.RegisterAsync(dto);

            if (!registerResult.IsSuccess)
                return Results.BadRequest(ResponseErrors.Register(details: registerResult.ErrorMessage));

            return Results.Ok("User Registered Successfully");
        })
        .EndPointConfigurations("Register New User")
        .OkRouteConfiguration()
        .ErrorRouteConfiguration();

        builder.MapPost("/login", async ([FromBody] LoginDto dto, IValidator<LoginDto> validator, IAuthService authService, HttpResponse response) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(ResponseErrors.Validation(details: validationResult.ToCustomString()));

            var loginResult = await authService.GetTokenAsync(dto);

            if (!loginResult.IsSuccess || loginResult.model is null)
                return Results.BadRequest(ResponseErrors.Login(details: loginResult.ErrorMessage));

            response.SetRefreshTokenCookie(
                expiresOn: loginResult.model.RefreshTokenExpiration,
                refreshToken: loginResult.model.RefreshToken);

            return Results.Ok(loginResult.model);
        })
        .EndPointConfigurations("User Login")
        .OkRouteConfiguration<AuthDto>()
        .ErrorRouteConfiguration();

        builder.MapPost("/refreshToken", async (IAuthService authService, HttpRequest request, HttpResponse response) =>
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
        })
        .EndPointConfigurations("Generate Refresh Token")
        .OkRouteConfiguration<AuthDto>()
        .ErrorRouteConfiguration();

        builder.MapPost("/resetPassword", [Authorize] async ([FromBody] ResetPasswordDto dto, IAuthService authService, IValidator<ResetPasswordDto> validator, ClaimsPrincipal user) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(ResponseErrors.Validation(details: validationResult.ToCustomString()));

            var resetPasswordResult = await authService.ResetPasswordAsync(dto, user.GetUserId());

            if (!resetPasswordResult.IsSuccess)
                return Results.BadRequest(ResponseErrors.Register(details: resetPasswordResult.ErrorMessage));

            return Results.Ok("Password Reset Successfully");
        })
        .EndPointConfigurations("Reset User Password")
        .OkRouteConfiguration()
        .ErrorRouteConfiguration();

        builder.MapPut("/Profile", [Authorize] async ([FromBody] ProfileUpdateDto dto, IAuthService authService, IValidator<ProfileUpdateDto> validator, ClaimsPrincipal user) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(ResponseErrors.Validation(details: validationResult.ToCustomString()));

            var updateProfileResult = await authService.UpdateProfileAsync(dto, user.GetUserId());

            if (!updateProfileResult.IsSuccess)
                return Results.BadRequest(ResponseErrors.Put(details: updateProfileResult.ErrorMessage));

            return Results.Ok("User Profile Updated Successfully");
        })
        .EndPointConfigurations("Update User Profile")
        .OkRouteConfiguration()
        .ErrorRouteConfiguration();

        builder.MapGet("/Profile", [Authorize] async (IAuthService authService, ClaimsPrincipal user) =>
        {
            var getProfileResult = await authService.GetProfileAsync(user.GetUserId());

            if (!getProfileResult.IsSuccess)
                return Results.BadRequest(ResponseErrors.Get(details: getProfileResult.ErrorMessage));

            return Results.Ok(getProfileResult.model);
        })
        .EndPointConfigurations("Get User Profile")
        .OkRouteConfiguration<ProfileDto>()
        .ErrorRouteConfiguration();

        return builder;
    }
}
