namespace BisHelpers.Domain.Entities;
public class AcademicSemester : BaseEntity
{
    public int Id { get; set; }

    public DateOnly startDate { get; set; }

    public DateOnly endDate { get; set; }

    public Semester? Semester { get; set; }
    public int SemesterId { get; set; }
}
