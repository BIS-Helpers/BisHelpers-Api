namespace BisHelpers.Application.Services.Student;
public interface IStudentService
{
    public Task<(bool IsSuccess, int? StudentId, string? ErrorMessage)> CreateAsync(RegisterDto model, string userId);
}
