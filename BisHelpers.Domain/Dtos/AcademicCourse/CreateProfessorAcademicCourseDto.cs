namespace BisHelpers.Domain.Dtos.AcademicCourse;

public class CreateProfessorAcademicCourseDto
{
    [Required]
    public int AcademicCourseId { get; set; }

    [Required]
    public int ProfessorId { get; set; }

    public List<CreateAcademicLectureDto> Lectures { get; set; } = [];
}
