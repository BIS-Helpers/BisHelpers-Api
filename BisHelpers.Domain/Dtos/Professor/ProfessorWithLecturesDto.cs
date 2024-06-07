using BisHelpers.Domain.Dtos.AcademicLecture;

namespace BisHelpers.Domain.Dtos.Professor;
public class ProfessorWithLecturesDto : ProfessorBaseDto
{
    public IEnumerable<AcademicLectureBaseDto> AcademicLectures { get; set; } = [];
}
