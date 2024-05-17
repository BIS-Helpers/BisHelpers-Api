namespace BisHelpers.Application.Services.Professor;

public interface IProfessorService
{
    public Task<Response<Domain.Entities.Professor>> Add(ProfessorCreateDto dto, string userId);
}
