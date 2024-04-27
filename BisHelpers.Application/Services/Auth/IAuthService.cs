namespace BisHelpers.Application.Services.Auth;

public interface IAuthService
{
    public Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterDto model);
}
