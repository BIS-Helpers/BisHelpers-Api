namespace BisHelpers.Domain.Dtos.AcademicLecture;
public class AcademicLectureWithProfessorDto : AcademicLectureBaseDto
{
    public ProfessorBaseDto? Professor { get; set; }
}
