namespace BisHelpers.Domain.Dtos.Student;
public class StudentBaseDto : BaseDto
{
    public int StudentId { get; set; }

    public string UserId { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public string Level { get; set; } = null!;

    public string CollegeId { get; set; } = null!;

    public double Gpa { get; set; }

    public int TotalEarnedHours { get; set; }
}
