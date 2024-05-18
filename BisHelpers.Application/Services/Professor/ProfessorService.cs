namespace BisHelpers.Application.Services.Professor;

public class ProfessorService(IUnitOfWork unitOfWork) : IProfessorService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

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

    public async Task<ProfessorDto> GetById(int id)
    {
        var professor = _unitOfWork.Professors.GetById(id);

        return professor.MapToDto();
    }
}
