using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.Domain.Dtos.AcademicLecture;
public class AcademicLectureWithProfessorAndCourseDto : AcademicLectureBaseDto
{
    public AcademicCourseBaseDto? AcademicCourse { get; set; }

    public ProfessorBaseDto? Professor { get; set; }
}
