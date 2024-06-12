namespace BisHelpers.Application.Services.AcademicSemesterService;
public interface IAcademicSemesterService
{
    public Task<int> GetCurrentAcademicSemesterIdAsync();

    public Task<string> GetCurrentAcademicSemesterNameAsync();
}
