namespace BisHelpers.Domain.Entities.RelatedData;

[Index(nameof(Name), IsUnique = true)]
public class Semester
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;
}
