namespace BisHelpers.Domain.Dtos.AcademicCourse;
public class AcademicCourseWithProfessorsDto : AcademicCourseBaseDto
{
    public List<ProfessorWithLecturesDto> Professors { get; set; } = [];
}
