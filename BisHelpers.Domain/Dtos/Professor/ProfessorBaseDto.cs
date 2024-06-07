namespace BisHelpers.Domain.Dtos.Professor;

public class ProfessorBaseDto : BaseDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;
}
