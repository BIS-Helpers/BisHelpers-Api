namespace BisHelpers.Domain.DefaultData;
public static class DefaultAcademicSemesters
{
    public static readonly List<AcademicSemester> Data =
    [
        new AcademicSemester { Id = 1, Name = "Fall" },
        new AcademicSemester { Id = 2, Name = "Winter" },
        new AcademicSemester { Id = 3, Name = "Spring" },
        new AcademicSemester { Id = 4, Name = "Summer" }
    ];
}
