using BisHelpers.Domain.Dtos.AcademicLecture;

namespace BisHelpers.Domain.Dtos.AcademicCourse;

public class AddProfessorToAcademicCourseDto
{
    [Required]
    public int AcademicCourseId { get; set; }

    [Required]
    public int ProfessorId { get; set; }

    public IEnumerable<CreateAcademicLectureDto> Lectures { get; set; } = [];
}
