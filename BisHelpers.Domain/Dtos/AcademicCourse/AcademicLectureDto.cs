namespace BisHelpers.Domain.Dtos.AcademicCourse;
public class AcademicLectureDto : BaseDto
{
    public int Id { get; set; }

    public string GroupNumber { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public string Day { get; set; } = null!;
}
