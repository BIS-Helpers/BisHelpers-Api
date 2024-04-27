namespace BisHelpers.Application.Services.Student;
public class StudentService(IUnitOfWork unitOfWork) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(bool IsSuccess, int? StudentId)> CreateAsync(RegisterDto model, string userId)
    {
        var student = model.MapToStudent();

        student.CreatedById = userId;
        student.UserId = userId;

        var addedStudent = _unitOfWork.Students.Add(student);

        if (addedStudent is null)
            return (IsSuccess: false, StudentId: null);

        return (IsSuccess: true, StudentId: addedStudent.Id);
    }
}
