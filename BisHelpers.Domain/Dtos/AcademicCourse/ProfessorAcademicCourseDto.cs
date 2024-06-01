using BisHelpers.Domain.Dtos.Professor;

namespace BisHelpers.Domain.Dtos.AcademicCourse;
public class ProfessorAcademicCourseDto : BaseDto
{
    public string? AcademicYear { get; set; }

    public string? AcademicSemester { get; set; }

    public ProfessorDto? Professor { get; set; }
}
