namespace BisHelpers.Domain.Dtos;

public class RegisterDto
{
    public string FullName { get; set; } = null!;

    public string CollegeId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public DateTime DateOfJoin { get; set; }

    public string Gender { get; set; } = null!;
}
