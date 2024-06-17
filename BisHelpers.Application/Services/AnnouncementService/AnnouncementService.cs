namespace BisHelpers.Application.Services.AnnouncementService;
public class AnnouncementService(IUnitOfWork unitOfWork, IAcademicSemesterService academicSemesterService) : IAnnouncementService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<IEnumerable<Announcement>> GetAllForStudentAsync(Student student)
    {
        var announcementQueryable = _unitOfWork.Announcements.GetQueryable();
        var lecturesIds = student.Registrations.SelectMany(r => r.Lectures.Select(l => l.AcademicLectureId));

        announcementQueryable = announcementQueryable
            .Include(a => a.AcademicLecture)
                .ThenInclude(a => a.ProfessorAcademicCourse)
                .ThenInclude(a => a.Professor)
            .Include(a => a.AcademicLecture)
                .ThenInclude(a => a.ProfessorAcademicCourse)
                .ThenInclude(a => a.AcademicSemester)
                .ThenInclude(a => a.Semester)
            .Include(a => a.AcademicLecture)
                .ThenInclude(a => a.ProfessorAcademicCourse)
                .ThenInclude(a => a.AcademicCourses);

        announcementQueryable = announcementQueryable
            .Where(a => lecturesIds.Contains(a.AcademicLectureId) && !a.IsDeleted);

        var announcements = await announcementQueryable.ToListAsync();

        return announcements;
    }
}
