namespace BisHelpers.Application.Services.AcademicSemester;
public interface IAcademicSemesterService
{
    public Task<int?> GetCurrentAcademicSemester();
}
