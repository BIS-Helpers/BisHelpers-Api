using BisHelpers.Application.Services.AcademicSemester;

namespace BisHelpers.Application.Services.Professor;

public class ProfessorService(IUnitOfWork unitOfWork, IAcademicSemesterService academicSemesterService) : IProfessorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<Response<Domain.Entities.Professor>> AddAsync(ProfessorCreateDto dto, string userId)
    {
        var professor = dto.MapToProfessor();

        professor.CreatedById = userId;

        _unitOfWork.Professors.Add(professor);
        await _unitOfWork.CompleteAsync();

        return new Response<Domain.Entities.Professor>
        {
            IsSuccess = true,
            Model = professor
        };
    }

    public async Task<IEnumerable<ProfessorDto>> GetAllAsync()
    {
        var professors = _unitOfWork.Professors.GetAll();

        return professors.MapToDto();
    }

    public async Task<IEnumerable<ProfessorDto>> GetAllAsync(int courseId)
    {
        var currentSemesterId = await _academicSemesterService.GetCurrentAcademicSemester();

        var professorQueryable = _unitOfWork.Professors.GetQueryable();

        var professors = await professorQueryable
            .Include(p => p.AcademicCourses)
                .ThenInclude(p => p.AcademicLectures)
            .Where(p =>
                p.AcademicCourses.Any(a => a.AcademicCourseId == courseId) &&
                p.AcademicCourses.Any(a => a.AcademicSemesterId == currentSemesterId))
            .ToListAsync();

        return professors.MapToDto();
    }

    public async Task<ProfessorDto> GetById(int id)
    {
        var professor = _unitOfWork.Professors.GetById(id);

        return professor.MapToDto();
    }
}
