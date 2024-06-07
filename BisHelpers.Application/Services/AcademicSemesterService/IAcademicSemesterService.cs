namespace BisHelpers.Application.Services.AcademicSemesterService;
public interface IAcademicSemesterService
{
    public Task<int?> GetCurrentAcademicSemester();
}
