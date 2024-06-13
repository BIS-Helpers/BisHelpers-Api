namespace BisHelpers.Domain.Dtos.Student;
public class GpaAnalysisDto
{
    public string FullName { get; set; } = null!;

    public string Level { get; set; } = null!;

    public string CollegeId { get; set; } = null!;

    public double Gpa { get; set; }

    public IEnumerable<AcademicLectureWithProfessorAndCourseDto> RegisteredAcademicLectures { get; set; } = [];

    public int TotalEarnedHours { get; set; }

    public double TotalEarnedPoints =>
        Gpa * TotalEarnedHours;

    public double TotalPoints =>
        4 * TotalEarnedHours;

    public int TotalCurrentHours =>
        TotalEarnedHours + RegisteredAcademicLectures?.Select(r =>
            r.AcademicCourse?.CreditHours).Sum() ?? 0;

    public double TotalCurrentPoints =>
        4 * TotalCurrentHours;

    public string MinGradeToSaveGpa { get; set; } = null!;

    public double PointsBasedOnMinGrade => Math.Round
        (RegisteredAcademicLectures?.Select(r =>
            r.AcademicCourse?.CreditHours * GradesPoints.GradesPointsDictionary.GetValueOrDefault(MinGradeToSaveGpa)).Sum() ?? 0, 2);

    public double GpaBasedOnMinGrade => Math.Round
        ((PointsBasedOnMinGrade + TotalEarnedPoints) / TotalCurrentHours, 2);

    public double MinPointsToSaveGpa => Math.Round
        ((Gpa * TotalCurrentHours) - TotalEarnedPoints, 2);
}
