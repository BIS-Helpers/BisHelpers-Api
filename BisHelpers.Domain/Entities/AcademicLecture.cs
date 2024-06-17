namespace BisHelpers.Domain.Entities;

[Index(nameof(GroupNumber), nameof(ProfessorAcademicCourseId), IsUnique = true)]
[Index(nameof(Day), nameof(StartTime), nameof(ProfessorAcademicCourseId), IsUnique = true)]
public class AcademicLecture : BaseEntity
{
    public int Id { get; set; }

    [StringLength(2)]
    public string GroupNumber { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public string Day { get; set; } = null!;

    #region Relations
    public ProfessorAcademicCourse? ProfessorAcademicCourse { get; set; }
    public int ProfessorAcademicCourseId { get; set; }

    public ICollection<RegistrationLecture> Registrations { get; set; } = [];

    public ICollection<Announcement> Announcements { get; set; } = [];
    #endregion
}
