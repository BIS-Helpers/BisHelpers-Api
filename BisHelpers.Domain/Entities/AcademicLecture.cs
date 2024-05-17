namespace BisHelpers.Domain.Entities;

[Index(nameof(GroupNumber), nameof(ProfessorAcademicCourseId), IsUnique = true)]
public class AcademicLecture : BaseEntity
{
    public int Id { get; set; }

    [StringLength(2)]
    public string GroupNumber { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public ProfessorAcademicCourse? ProfessorAcademicCourse { get; set; }
    public int ProfessorAcademicCourseId { get; set; }
}
