namespace BisHelpers.Application.Services.Auth;

public interface IAuthService
{
    public Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterDto model);
    public Task<(bool IsSuccess, ProfileDto? model, string? ErrorMessage)> GetProfileAsync(string userId);
    public Task<(bool IsSuccess, AuthDto? model, string? ErrorMessage)> GetTokenAsync(LoginDto model);
    public Task<(bool IsSuccess, AuthDto? model, string? ErrorMessage)> RefreshTokenAsync(string token);
}
