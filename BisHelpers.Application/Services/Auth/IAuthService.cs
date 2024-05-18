namespace BisHelpers.Application.Services.Auth;

public interface IAuthService
{
    public Task<Response> RegisterAsync(RegisterDto model);
    public Task<Response<ProfileDto>> GetProfileAsync(string userId);
    public Task<Response<AuthDto>> GetTokenAsync(LoginDto model);
    public Task<Response<AuthDto>> RefreshTokenAsync(string token);
    public Task<Response> ResetPasswordAsync(ResetPasswordDto model, string userId);
    public Task<Response> UpdateProfileAsync(ProfileUpdateDto model, string userId);
}
