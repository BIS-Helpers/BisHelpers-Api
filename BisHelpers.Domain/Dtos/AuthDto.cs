namespace BisHelpers.Domain.Dtos;

public class AuthDto
{
    public string FullName { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string AcademicYear { get; set; } = null!;

    public bool? HasActiveRegistration { get; set; }

    public IEnumerable<string> Roles { get; set; } = [];

    public string Token { get; set; } = null!;

    public DateTime ExpiresOn { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;

    public DateTime RefreshTokenExpiration { get; set; }
}
