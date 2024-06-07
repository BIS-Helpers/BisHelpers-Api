namespace BisHelpers.Domain.Dtos.AcademicCourse;
public class AcademicCourseBaseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int CreditHours { get; set; }
}
