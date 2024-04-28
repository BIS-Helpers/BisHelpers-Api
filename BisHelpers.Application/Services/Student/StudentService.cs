namespace BisHelpers.Application.Services.Student;
public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(bool IsSuccess, int? StudentId, string? ErrorMessage)> CreateAsync(RegisterDto model, string userId)
    {
        var student = model.MapToStudent();

        student.CreatedById = userId;
        student.UserId = userId;

        var addedStudent = _unitOfWork.Students.Add(student);
        await _unitOfWork.CompleteAsync();

        if (addedStudent is null)
            return (IsSuccess: false, StudentId: null, ErrorMessage: "user is not found!");

        return (IsSuccess: true, StudentId: addedStudent.Id, ErrorMessage: null);
    }
}
