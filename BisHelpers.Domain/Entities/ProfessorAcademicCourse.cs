namespace BisHelpers.Domain.Entities;

[Index(nameof(AcademicSemesterId), nameof(AcademicCoursesId), nameof(ProfessorId), nameof(Year), IsUnique = true)]
public class ProfessorAcademicCourse : BaseEntity
{
    #region Prperties
    public int Id { get; set; }

    public int Year { get; set; }
    #endregion

    #region Relations
    public AcademicCourse? AcademicCourses { get; set; }
    public int AcademicCoursesId { get; set; }

    public AcademicSemester? AcademicSemester { get; set; }
    public int AcademicSemesterId { get; set; }

    public Professor? Professor { get; set; }
    public int ProfessorId { get; set; }

    public ICollection<AcademicLecture> AcademicLectures { get; set; } = [];
    #endregion
}
