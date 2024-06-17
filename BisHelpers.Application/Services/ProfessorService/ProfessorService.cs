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
            .Include(p => p.CreatedBy)
            .Include(p => p.LastUpdatedBy)
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
                p.AcademicCourses.Any(a => a.AcademicCourseId == courseId && a.AcademicSemesterId == currentSemesterId) && !p.IsDeleted)
            .Select(p => new Professor
            {
                Id = p.Id,
                FullName = p.FullName,
                AcademicCourses = p.AcademicCourses
                .Where(c =>
                    c.AcademicCourseId == courseId &&
                    c.AcademicSemesterId == currentSemesterId)
                .ToList()
            }).ToListAsync();

        return professors;
    }

    public async Task<Professor?> GetByIdWithAcademicCoursesAndAcademicLecturesAsync(int id)
    {
        var professor = _unitOfWork.Professors.Find(
            predicate: p => p.Id == id && !p.IsDeleted,
            include: p => p.Include(p => p.AcademicCourses).ThenInclude(a => a.AcademicLectures));

        return professor;
    }

    public async Task<Professor?> GetById(int id)
    {
        var professorQueryable = _unitOfWork.Professors.GetQueryable();

        var professors = await professorQueryable
            .Include(p => p.CreatedBy)
            .Include(p => p.LastUpdatedBy)
            .AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

        return professors;
    }

    public async Task<Response<Professor>> UpdateAsync(ProfessorUpdateDto dto, Professor professor, string userId)
    {
        professor.FullName = dto.FullName;
        professor.LastUpdatedById = userId;
        professor.LastUpdatedOn = DateTime.UtcNow;

        _unitOfWork.Professors.Update(professor);
        await _unitOfWork.CompleteAsync();

        return new Response<Professor> { IsSuccess = true, Model = professor };
    }

    public async Task<Response<Professor>> ToggleStatusAsync(Professor professor, string userId)
    {
        professor.IsDeleted = !professor.IsDeleted;
        professor.LastUpdatedById = userId;
        professor.LastUpdatedOn = DateTime.UtcNow;

        _unitOfWork.Professors.Update(professor);
        await _unitOfWork.CompleteAsync();

        return new Response<Professor> { IsSuccess = true, Model = professor };
    }

}
