namespace BisHelpers.Domain.Entities;
public class AcademicSemester : BaseEntity
{
    public int Id { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Semester? Semester { get; set; }
    public int SemesterId { get; set; }
}
