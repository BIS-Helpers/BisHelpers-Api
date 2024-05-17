namespace BisHelpers.Domain.Dtos.Professor;

public class ProfessorCreateDto
{
    [Required]
    [StringLength(100)]
    [RegularExpression(RegexPatterns.CharactersOnly_Eng)]
    public string FullName { get; set; } = null!;
}
