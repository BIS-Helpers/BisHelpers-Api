using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.Application.Services.StudentService;
public class StudentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IAcademicSemesterService academicSemesterService) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<Student?> GetStudentAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null || user.Student is null)
            return null;

        return user.Student;
    }

    public async Task<Response> RegisterAcademicLecturesAsync(string userId, RegisterAcademicLecturesDto dto)
    {
        var student = await GetStudentAsync(userId);

        if (student is null)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not register academic lectures", Details = ["user not found"] } };

        var detailedStudent = _unitOfWork.Students.Find(
            predicate: s => !s.IsDeleted && s.Id == student.Id,
            include: s => s.Include(s => s.Registrations).ThenInclude(r => r.Lectures).ThenInclude(l => l.AcademicLecture).ThenInclude(a => a.ProfessorAcademicCourse)!);

        if (detailedStudent is null)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not register academic lectures", Details = ["student not found"] } };

        var result = await IsStudentHasActiveRegistration(student);

        if (result)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not register academic lectures", Details = ["student Has Active Academic Registration"] } };

        var registration = new AcademicRegistration
        {
            Gpa = dto.Gpa,
            TotalEarnedHours = dto.TotalEarnedHours,
            CreatedById = userId
        };

        foreach (var lecturesId in dto.LecturesIds)
            registration.Lectures.Add(new RegistrationLecture { AcademicLectureId = lecturesId });

        student.Registrations.Add(registration);

        _unitOfWork.Students.Update(student);
        await _unitOfWork.CompleteAsync();

        return new Response { IsSuccess = true };
    }

    private async Task<bool> IsStudentHasActiveRegistration(Student student)
    {
        var id = await _academicSemesterService.GetCurrentAcademicSemester();

        if (id is null)
            return false;

        var result = student.Registrations.SelectMany(r =>
            r.Lectures.Select(l => l.AcademicLecture?.ProfessorAcademicCourse?.AcademicSemesterId)).Contains((int)id);

        return result;
    }

    //TODO: Update Return 
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
