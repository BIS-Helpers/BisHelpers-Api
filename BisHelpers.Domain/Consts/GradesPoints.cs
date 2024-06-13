namespace BisHelpers.Domain.Consts;
public static class GradesPoints
{
    public static Dictionary<string, double> GradesPointsDictionary => new()
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
}
