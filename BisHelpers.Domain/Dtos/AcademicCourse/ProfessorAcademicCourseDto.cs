using BisHelpers.Domain.Dtos.Professor;

namespace BisHelpers.Domain.Dtos.AcademicCourse;
public class ProfessorAcademicCourseDto : BaseDto
{
    public string? AcademicYear { get; set; }

    public string? AcademicCourse { get; set; }

    public string? AcademicSemester { get; set; }

    public ProfessorWithLecturesDto? Professor { get; set; }
}
