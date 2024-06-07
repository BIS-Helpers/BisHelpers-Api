using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.Domain.Dtos.Professor;
public class ProfessorWithLecturesDto : ProfessorBaseDto
{
    public IEnumerable<AcademicLectureDto> AcademicLectures { get; set; } = [];
}
