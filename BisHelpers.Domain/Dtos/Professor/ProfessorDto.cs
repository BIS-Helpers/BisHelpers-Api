using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.Domain.Dtos.Professor;
public class ProfessorDto
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public IEnumerable<AcademicLectureDto> academicLectures { get; set; } = [];

}
