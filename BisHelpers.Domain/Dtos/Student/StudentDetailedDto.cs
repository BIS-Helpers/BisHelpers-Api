namespace BisHelpers.Domain.Dtos.Student;
public class StudentDetailedDto : StudentBaseDto
{
    public IEnumerable<AcademicLectureWithProfessorAndCourseDto> RegisteredAcademicLectures { get; set; } = [];
}
