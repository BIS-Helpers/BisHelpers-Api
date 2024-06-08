namespace BisHelpers.Domain.Dtos.Student;
public class RegisterAcademicLecturesDto
{
    [Required]
    public double Gpa { get; set; }

    [Required]
    public int TotalEarnedHours { get; set; }

    [Required]
    public List<int> LecturesIds { get; set; } = [];
}
