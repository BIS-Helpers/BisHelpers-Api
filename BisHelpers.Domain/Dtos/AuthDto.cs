namespace BisHelpers.Domain.Dtos;

public class AuthDto
{
    [Required]
    public string FullName { get; set; } = null!;

    [Required]
    public string Gender { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public IEnumerable<string> Roles { get; set; } = [];

    [Required]
    public string Token { get; set; } = null!;

    [Required]
    public DateTime? ExpiresOn { get; set; }

    [Required]
    [JsonIgnore]
    public string RefreshToken { get; set; } = null!;

    [Required]
    public DateTime RefreshTokenExpiration { get; set; }
}
