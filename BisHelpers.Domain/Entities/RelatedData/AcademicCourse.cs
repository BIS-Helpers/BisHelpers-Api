namespace BisHelpers.Domain.Entities.RelatedData;

[Index(nameof(Name), IsUnique = true)]
[Index(nameof(Code), IsUnique = true)]
public class AcademicCourse
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string Code { get; set; } = null!;

    public int CreditHours { get; set; }

    public ICollection<ProfessorAcademicCourse> Professors { get; set; } = [];
}
