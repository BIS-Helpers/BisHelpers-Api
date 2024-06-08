namespace BisHelpers.Domain.Dtos.Student;
public class GpaAnalysisDto
{
    public string FullName { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string CollegeId { get; set; } = null!;

    public double Gpa { get; set; }

    public int TotalEarnedHours { get; set; }

    public IEnumerable<AcademicLectureWithProfessorAndCourseDto>? RegisteredAcademicLectures { get; set; } = [];

    public string MinGradeToSaveGpa { get; set; } = null!;

    public int TotalCurrentHours =>
        TotalEarnedHours + RegisteredAcademicLectures?.Select(r =>
            r.AcademicCourse?.CreditHours).Sum() ?? 0;

    public double TotalEarnedPoints =>
        Gpa * TotalEarnedHours;

    public double TotalPoints =>
        4 * TotalEarnedHours;

    public double MinPointsToSaveGpa =>
        (Gpa * TotalCurrentHours) - TotalEarnedPoints;
}
