namespace BisHelpers.Domain.Entities;

[Index(nameof(StudentId), nameof(AcademicLectureId), IsUnique = true)]
public class AcademicRegistration : BaseEntity
{
    public int Id { get; set; }

    public Student? Student { get; set; }
    public int StudentId { get; set; }

    public AcademicLecture? AcademicLecture { get; set; }
    public int AcademicLectureId { get; set; }
}
