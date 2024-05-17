namespace BisHelpers.Domain.Entities.RelatedData;

[Index(nameof(Name), IsUnique = true)]
public class AcademicSemester
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
}
