namespace BisHelpers.Domain.Dtos;

public class LoginDto
{
    [Required]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}
