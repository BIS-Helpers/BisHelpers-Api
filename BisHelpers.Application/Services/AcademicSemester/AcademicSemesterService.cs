namespace BisHelpers.Application.Services.AcademicSemester;
public class AcademicSemesterService(IUnitOfWork unitOfWork) : IAcademicSemesterService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<int?> GetCurrentAcademicSemester()
    {
        var semesterQueryable = _unitOfWork.AcademicSemesters.GetQueryable();

        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);

        var semester = await semesterQueryable
            .Include(s => s.Semester)
            .Where(s => s.startDate <= currentDate && s.endDate >= currentDate && !s.IsDeleted)
            .FirstOrDefaultAsync();

        if (semester is null)
            return null;

        return semester.Id;
    }
}
