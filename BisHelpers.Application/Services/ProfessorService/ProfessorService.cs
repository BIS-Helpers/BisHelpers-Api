namespace BisHelpers.Application.Services.ProfessorService;

public class ProfessorService(IUnitOfWork unitOfWork, IAcademicSemesterService academicSemesterService) : IProfessorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<Response<Professor>> AddAsync(ProfessorCreateDto dto, string userId)
    {
        var professor = dto.ToProfessor();

        professor.CreatedById = userId;

        _unitOfWork.Professors.Add(professor);
        await _unitOfWork.CompleteAsync();

        return new Response<Professor>
        {
            IsSuccess = true,
            Model = professor
        };
    }

    public async Task<IEnumerable<Professor>> GetAllAsync()
    {
        var professorQueryable = _unitOfWork.Professors.GetQueryable();

        var professors = await professorQueryable
            .Where(p => !p.IsDeleted)
            .AsNoTracking().ToListAsync();

        return professors;
    }

    public async Task<IEnumerable<Professor>> GetAllAsync(int courseId)
    {
        var currentSemesterId = await _academicSemesterService.GetCurrentAcademicSemesterIdAsync();

        var professorQueryable = _unitOfWork.Professors.GetQueryable();

        var professors = await professorQueryable
            .Include(p => p.AcademicCourses)
                .ThenInclude(p => p.AcademicLectures)
            .Where(p =>
                p.AcademicCourses.Any(a => a.AcademicCourseId == courseId) &&
                p.AcademicCourses.Any(a => a.AcademicSemesterId == currentSemesterId) && !p.IsDeleted)
            .ToListAsync();

        return professors;
    }

    public async Task<Professor?> GetById(int id)
    {
        var professor = _unitOfWork.Professors.Find(
            predicate: p => p.Id == id && !p.IsDeleted,
            include: p => p.Include(p => p.AcademicCourses).ThenInclude(a => a.AcademicLectures));

        return professor;
    }
}
