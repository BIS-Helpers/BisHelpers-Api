namespace BisHelpers.Application.Services.StudentService;
public class StudentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<Student?> GetStudentAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null || user.Student is null)
            return null;

        return user.Student;
    }

    public async Task<Response> RegisterAcademicLecturesAsync(string userId, IEnumerable<int> lecturesIds)
    {
        var student = await GetStudentAsync(userId);

        if (student is null)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not register academic lectures", Details = ["user not found"] } };

        foreach (var lecturesId in lecturesIds)
            student.AcademicLectures.Add(new AcademicRegistration { AcademicLectureId = lecturesId, CreatedById = userId });

        _unitOfWork.Students.Update(student);
        await _unitOfWork.CompleteAsync();

        return new Response { IsSuccess = true };
    }

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
