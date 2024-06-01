namespace BisHelpers.Application.Services.Professor;

public interface IProfessorService
{
    public Task<Response<Domain.Entities.Professor>> AddAsync(ProfessorCreateDto dto, string userId);

    public Task<IEnumerable<ProfessorDto>> GetAllAsync();

    public Task<IEnumerable<ProfessorDto>> GetAllAsync(int courseId);

    public Task<ProfessorDto> GetById(int id);

}
