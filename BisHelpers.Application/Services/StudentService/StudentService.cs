using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.Application.Services.StudentService;
public class StudentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IAcademicSemesterService academicSemesterService) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<Student?> GetStudentIdByUserIdAsync(string userId)
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
        var student = _unitOfWork.Students.Find(
            predicate: s => !s.IsDeleted && s.UserId == userId,
            include: s => s.Include(s => s.Registrations).ThenInclude(r => r.Lectures).ThenInclude(l => l.AcademicLecture).ThenInclude(a => a.ProfessorAcademicCourse)!);

        if (student is null)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not register academic lectures", Details = ["student not found"] } };

        var result = await IsStudentHasActiveRegistrationAsync(student);

        if (result)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not register academic lectures", Details = ["student Has Active Academic Registration"] } };

        var registration = new AcademicRegistration
        {
            Gpa = dto.Gpa,
            TotalEarnedHours = dto.TotalEarnedHours,
            CreatedById = student.UserId
        };

        foreach (var lecturesId in dto.LecturesIds)
            registration.Lectures.Add(new RegistrationLecture { AcademicLectureId = lecturesId });

        student.Registrations.Add(registration);

        _unitOfWork.Students.Update(student);
        await _unitOfWork.CompleteAsync();

        return new Response { IsSuccess = true };
    }

    public async Task<AppUser?> GetDetailedStudentUserByUserIdAsync(string userId)
    {
        var student = await _userManager.Users
            .Include(u => u.Student)
                .ThenInclude(u => u.Registrations)
                    .ThenInclude(u => u.Lectures)
                        .ThenInclude(a => a.AcademicLecture)
                            .ThenInclude(a => a.ProfessorAcademicCourse)
                                .ThenInclude(p => p.Professor)
            .Include(u => u.Student)
                .ThenInclude(u => u.Registrations)
                    .ThenInclude(u => u.Lectures)
                        .ThenInclude(a => a.AcademicLecture)
                            .ThenInclude(a => a.ProfessorAcademicCourse)
                             .ThenInclude(p => p.AcademicSemester)
                                .ThenInclude(a => a.Semester)
            .Include(u => u.Student)
                .ThenInclude(u => u.Registrations)
                    .ThenInclude(u => u.Lectures)
                        .ThenInclude(a => a.AcademicLecture)
                            .ThenInclude(a => a.ProfessorAcademicCourse)
                                .ThenInclude(p => p.AcademicCourses)
            .Where(u => !u.IsDeleted)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (student is null || student.IsDeleted)
            return null;

        return student;
    }

    public async Task<bool> IsStudentHasActiveRegistrationAsync(string userId)
    {
        var id = await _academicSemesterService.GetCurrentAcademicSemester();

        if (id is null)
            return false;

        var student = _unitOfWork.Students.Find(
            predicate: s => !s.IsDeleted && s.UserId == userId,
            include: s => s.Include(s => s.Registrations).ThenInclude(r => r.Lectures).ThenInclude(l => l.AcademicLecture).ThenInclude(a => a.ProfessorAcademicCourse)!);

        if (student is null)
            return false;

        var result = student.Registrations.SelectMany(r =>
        r.Lectures.Select(l => l.AcademicLecture?.ProfessorAcademicCourse?.AcademicSemesterId)).Contains((int)id);

        return result;
    }


    private async Task<bool> IsStudentHasActiveRegistrationAsync(Student student)
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
