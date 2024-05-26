namespace BisHelpers.Domain.Dtos.AcademicCourse;

public class ProfessorAcademicCourseDto
{
    [Required]
    [Range(1, 9999)]
    public int Year { get; set; }

    [Required]
    public int AcademicCourseId { get; set; }

    [Required]
    public int AcademicSemesterId { get; set; }

    [Required]
    public int ProfessorId { get; set; }

    public List<AcademicLectureDto> Lectures { get; set; } = [];
}
