using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.Application.Services.StudentService;
public class StudentService(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, IAcademicSemesterService academicSemesterService) : IStudentService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<Student?> GetStudentByUserIdAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null || user.Student is null)
            return null;

        return user.Student;
    }

    public async Task<Response> RegisterAcademicLecturesAsync(Student student, RegisterAcademicLecturesDto dto)
    {
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

    public async Task<AppUser?> GetDetailedStudentUserByUserIdAsync(string userId, bool includeDeleted = false)
    {
        IQueryable<AppUser> studentUserQueryable = _userManager.Users
            .Include(u => u.Student)
                .ThenInclude(s => s.CreatedBy)
            .Include(u => u.Student)
                .ThenInclude(s => s.LastUpdatedBy)
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
                                .ThenInclude(p => p.AcademicCourses);


        if (!includeDeleted)
            studentUserQueryable = studentUserQueryable.Where(u => !u.IsDeleted);

        var student = await studentUserQueryable.FirstOrDefaultAsync(u => u.Id == userId);

        return student;
    }

    public async Task<bool> IsStudentHasActiveRegistrationAsync(string userId)
    {
        var id = await _academicSemesterService.GetCurrentAcademicSemesterIdAsync();

        var student = _unitOfWork.Students.Find(
            predicate: s => !s.IsDeleted && s.UserId == userId,
            include: s => s.Include(s => s.Registrations).ThenInclude(r => r.Lectures).ThenInclude(l => l.AcademicLecture).ThenInclude(a => a.ProfessorAcademicCourse)!);

        if (student is null)
            return false;

        var result = student.Registrations.SelectMany(r =>
        r.Lectures.Select(l => l.AcademicLecture?.ProfessorAcademicCourse?.AcademicSemesterId)).Contains(id);

        return result;
    }

    public async Task<bool> IsStudentHasActiveRegistrationAsync(Student student)
    {
        var id = await _academicSemesterService.GetCurrentAcademicSemesterIdAsync();

        if (id == 0)
            return false;

        var result = student.Registrations.SelectMany(r =>
            r.Lectures.Select(l => l.AcademicLecture?.ProfessorAcademicCourse?.AcademicSemesterId)).Contains(id);

        return result;
    }

    public async Task<Response> DropActiveRegistrationAsync(Student student)
    {
        var id = await _academicSemesterService.GetCurrentAcademicSemesterIdAsync();

        var activeAcademicRegistration = student.Registrations
            .FirstOrDefault(r => r.Lectures.Select(l => l.AcademicLecture?.ProfessorAcademicCourse?.AcademicSemesterId).Contains(id));

        if (activeAcademicRegistration is null)
            return new Response { ErrorBody = new ErrorBody { Message = "Can not drop active registration", Details = ["student has not active registration"] } };

        student.Registrations.Remove(activeAcademicRegistration);

        _unitOfWork.Students.Update(student);
        await _unitOfWork.CompleteAsync();

        return new Response { IsSuccess = true };
    }

    public async Task<IEnumerable<AppUser>> GetAllAsync()
    {
        var students = await _userManager.Users
            .Include(u => u.Student)
                .ThenInclude(s => s.CreatedBy)
            .Include(u => u.Student)
                .ThenInclude(s => s.LastUpdatedBy)
            .Include(u => u.Student)
                .ThenInclude(s => s.Registrations)
            .ToListAsync();

        return students;
    }

    public async Task<Response> ToggleStatusAsync(Student student, string userId)
    {
        student.IsDeleted = !student.IsDeleted;
        student.LastUpdatedById = userId;
        student.LastUpdatedOn = DateTime.UtcNow;

        _unitOfWork.Students.Update(student);
        await _unitOfWork.CompleteAsync();

        return new Response { IsSuccess = true };
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
