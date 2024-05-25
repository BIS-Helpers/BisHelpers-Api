namespace BisHelpers.Application.Extensions;
public static class DateExtensions
{
    public static string? ToCollegeLevel(this DateOnly dateOfJoin)
    {
        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow.Date);

        if (currentDate < dateOfJoin)
            return null;

        var yearsDiff = currentDate.Year - dateOfJoin.Year;

        if (yearsDiff > 4)
            return "Level 4";

        if (currentDate.Month >= 9 && currentDate.Month <= 12)
            return $"Level {yearsDiff + 1}";

        return $"Level {yearsDiff}";
    }

    public static string GetCurrentAcademicYear(this int year) =>
        string.Join('/', year.ToString(), (year + 1).ToString());

}
