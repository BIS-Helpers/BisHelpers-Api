namespace BisHelpers.Domain.Entities;

public class AcademicRegistration : BaseEntity
{
    public int Id { get; set; }

    public double Gpa { get; set; }

    public int TotalEarnedHours { get; set; }

    public Student? Student { get; set; }
    public int StudentId { get; set; }

    public ICollection<RegistrationLecture> Lectures { get; set; } = [];
}
