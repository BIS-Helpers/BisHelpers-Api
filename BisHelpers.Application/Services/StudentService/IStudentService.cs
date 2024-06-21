using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.Application.Services.StudentService;
public interface IStudentService
{
    public Task<Response> RegisterAcademicLecturesAsync(Student student, RegisterAcademicLecturesDto dto);

    public Task<Student?> GetStudentByUserIdAsync(string userId);

    public Task<AppUser?> GetDetailedStudentUserByUserIdAsync(string userId, bool includeDeleted = false);

    public Task<bool> IsStudentHasActiveRegistrationAsync(string userId);

    public Task<bool> IsStudentHasActiveRegistrationAsync(Student student);

    public Task<Response> DropActiveRegistrationAsync(Student student);

    public Task<IEnumerable<AppUser>> GetAllAsync();

    public Task<Response> ToggleStatusAsync(Student student, string userId);

    public Task<(bool IsSuccess, int? StudentId, string? ErrorMessage)> CreateAsync(RegisterDto model, string userId);
}
