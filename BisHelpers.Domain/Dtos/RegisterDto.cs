namespace BisHelpers.Domain.Dtos;

public class RegisterDto
{
    [Required]
    [StringLength(200)]
    [RegularExpression(RegexPatterns.CharactersOnly_Eng)]
    public string FullName { get; set; } = null!;

    [Required]
    [StringLength(9)]
    [RegularExpression(RegexPatterns.CollageId)]
    public string CollegeId { get; set; } = null!;

    [Required]
    [StringLength(128)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(256)]
    [RegularExpression(RegexPatterns.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [StringLength(11)]
    [RegularExpression(RegexPatterns.MobileNumber)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public DateOnly BirthDate { get; set; }

    [Required]
    public DateOnly DateOfJoin { get; set; }

    [Required]
    [RegularExpression(RegexPatterns.MaleOrFemaleOnly)]
    public string Gender { get; set; } = null!;
}
