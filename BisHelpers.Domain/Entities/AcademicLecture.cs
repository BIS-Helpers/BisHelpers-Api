namespace BisHelpers.Domain.Entities;

[Index(nameof(GroupNumber), nameof(ProfessorAcademicCourseId), IsUnique = true)]
public class AcademicLecture : BaseEntity
{
    public int Id { get; set; }

    [StringLength(2)]
    public string GroupNumber { get; set; } = null!;

    public DateTime DateTime { get; set; }

    #region Relations
    public ProfessorAcademicCourse? ProfessorAcademicCourse { get; set; }
    public int ProfessorAcademicCourseId { get; set; }

    public ICollection<AcademicRegistration> Students { get; set; } = [];
    #endregion
}
