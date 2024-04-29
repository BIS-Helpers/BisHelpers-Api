namespace BisHelpers.Domain.Dtos;

public class AuthDto
{
    public string Token { get; set; } = null!;

    public DateTime? ExpiresOn { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiration { get; set; }
}
