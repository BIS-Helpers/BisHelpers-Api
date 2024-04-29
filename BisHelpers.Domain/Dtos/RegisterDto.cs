using BisHelpers.Domain.Consts;

namespace BisHelpers.Domain.Dtos;

public class RegisterDto
{
    [StringLength(200)]
    [RegularExpression(RegexPatterns.CharactersOnly_Eng)]
    public string FullName { get; set; } = null!;

    [MaxLength(9)]
    [RegularExpression(RegexPatterns.CollageId)]
    public string CollegeId { get; set; } = null!;

    [Required]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [MaxLength(256)]
    [RegularExpression(RegexPatterns.Password)]
    public string Password { get; set; } = null!;

    [MaxLength(11)]
    [RegularExpression(RegexPatterns.MobileNumber)]
    public string PhoneNumber { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public DateTime DateOfJoin { get; set; }

    public string Gender { get; set; } = null!;
}
