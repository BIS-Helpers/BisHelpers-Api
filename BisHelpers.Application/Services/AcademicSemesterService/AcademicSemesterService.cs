namespace BisHelpers.Application.Services.AcademicSemesterService;
public class AcademicSemesterService(IUnitOfWork unitOfWork) : IAcademicSemesterService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    private async Task<AcademicSemester?> GetCurrentAcademicSemesterAsync()
    {
        var semesterQueryable = _unitOfWork.AcademicSemesters.GetQueryable();

        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var semester = await semesterQueryable
            .Include(s => s.Semester)
            .Where(s => s.StartDate <= currentDate && s.EndDate >= currentDate && !s.IsDeleted)
            .FirstOrDefaultAsync();

        return semester;
    }

    public async Task<int> GetCurrentAcademicSemesterIdAsync()
    {
        var semester = await GetCurrentAcademicSemesterAsync();

        if (semester is null)
            return 0;

        return semester.Id;
    }

    public async Task<string> GetCurrentAcademicSemesterNameAsync()
    {
        var semester = await GetCurrentAcademicSemesterAsync();

        if (semester is null || semester.Semester is null)
            return string.Empty;

        return semester.Semester.Name;
    }
}
