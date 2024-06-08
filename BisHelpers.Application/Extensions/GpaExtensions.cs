namespace BisHelpers.Application.Extensions;
public static class GpaExtensions
{
    public static string GetMinGrade(this IEnumerable<AcademicCourse> academicCourses, double MinPoints)
    {
        Dictionary<string, double> gradesPoints = new()
        {
            { "A+", 4 },
            { "A", 3.75},
            { "B+", 3.5},
            { "B", 3.1},
            { "C+", 2.8},
            { "C", 2.5},
            { "D+", 2.25},
            { "D", 2 },
            { "F", 0 }
        };

        string previousKey = string.Empty;

        foreach (var gradesPoint in gradesPoints)
        {
            var totalPoints = 0.00;

            foreach (var academicCourse in academicCourses)
                totalPoints += gradesPoint.Value * academicCourse.CreditHours;

            if (totalPoints < MinPoints)
                break;

            previousKey = gradesPoint.Key;
        }

        return previousKey;
    }
}
