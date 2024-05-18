namespace BisHelpers.Application.Services.Professor;

public interface IProfessorService
{
    public Task<Response<Domain.Entities.Professor>> AddAsync(ProfessorCreateDto dto, string userId);

    public Task<IEnumerable<ProfessorDto>> GetAllAsync();

    public Task<ProfessorDto> GetById(int id);

}
