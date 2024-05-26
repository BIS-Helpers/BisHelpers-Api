namespace BisHelpers.Domain.Dtos.AcademicCourse;

public class AcademicLectureDto
{
    [Required]
    [RegularExpression(RegexPatterns.NumbersOnly)]
    [StringLength(2)]
    public string GroupNumber { get; set; } = null!;

    [Required]
    public TimeOnly StartTime { get; set; }

    [Required]
    [RegularExpression(RegexPatterns.WeekDaysOnly)]
    public string Day { get; set; } = null!;
}
