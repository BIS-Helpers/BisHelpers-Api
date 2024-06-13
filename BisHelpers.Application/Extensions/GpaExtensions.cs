namespace BisHelpers.Application.Extensions;
public static class GpaExtensions
{
    public static string GetMinGrade(this IEnumerable<AcademicCourse> academicCourses, double MinPoints)
    {

        string previousKey = string.Empty;

        foreach (var gradesPoint in GradesPoints.GradesPointsDictionary)
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
