namespace BisHelpers.Application.Services.ProfessorService;

public interface IProfessorService
{
    public Task<Response<Professor>> AddAsync(ProfessorCreateDto dto, string userId);

    public Task<IEnumerable<Professor>> GetAllAsync();

    public Task<IEnumerable<Professor>> GetAllAsync(int courseId);

    public Task<Professor?> GetById(int id);
}
