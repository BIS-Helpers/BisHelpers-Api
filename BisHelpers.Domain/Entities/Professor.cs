namespace BisHelpers.Domain.Entities;

public class Professor : BaseEntity
{
    public int Id { get; set; }

    [StringLength(100)]
    public string FullName { get; set; } = null!;

    public ICollection<ProfessorAcademicCourse> AcademicCourses { get; set; } = [];
}
