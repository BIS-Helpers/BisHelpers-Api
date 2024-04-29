namespace BisHelpers.Domain.Dtos;

public class ResetPasswordDto
{
    public string OldPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;
}
