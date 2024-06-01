namespace BisHelpers.Domain.DefaultData;
public static class DefaultAcademicSemesters
{
    public static readonly List<Semester> Data =
    [
        new Semester { Id = 1, Name = "Fall" },
        new Semester { Id = 2, Name = "Winter" },
        new Semester { Id = 3, Name = "Spring" },
        new Semester { Id = 4, Name = "Summer" }
    ];
}
