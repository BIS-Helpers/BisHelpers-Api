using BisHelpers.Domain.Dtos.Announcement;

namespace BisHelpers.Application.Services.AnnouncementService;
public class AnnouncementService(IUnitOfWork unitOfWork, IAcademicSemesterService academicSemesterService) : IAnnouncementService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<IEnumerable<Announcement>> GetAllAsync(Student student)
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
                .ThenInclude(a => a.AcademicCourses)
                .OrderByDescending(a => a.CreatedOn);

        announcementQueryable = announcementQueryable
            .Where(a => (a.AcademicLectureId == null || lecturesIds.Contains((int)a.AcademicLectureId)) && !a.IsDeleted);

        var announcements = await announcementQueryable.ToListAsync();

        return announcements;
    }

    public async Task<Announcement?> GetByIdAsync(int id)
    {
        var announcementQueryable = _unitOfWork.Announcements.GetQueryable();

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

        var announcement = await announcementQueryable.FirstOrDefaultAsync(a => a.Id == id);

        return announcement;
    }

    public async Task<Response<Announcement>> AddAsync(AnnouncementCreateDto dto, string userId)
    {
        var announcement = dto.ToAnnouncement();

        announcement.CreatedById = userId;

        _unitOfWork.Announcements.Add(announcement);
        await _unitOfWork.CompleteAsync();

        return new Response<Announcement> { IsSuccess = true, Model = announcement };
    }

    public async Task<Response<Announcement>> UpdateAsync(AnnouncementUpdateDto dto, Announcement announcement, string userId)
    {
        announcement.Content = dto.Content;
        announcement.Title = dto.Title;
        announcement.AcademicLectureId = dto.AcademicLectureId;
        announcement.LastUpdatedById = userId;
        announcement.LastUpdatedOn = DateTime.UtcNow;

        _unitOfWork.Announcements.Update(announcement);
        await _unitOfWork.CompleteAsync();

        return new Response<Announcement> { IsSuccess = true, Model = announcement };
    }

    public async Task<Response<Announcement>> ToggleStatusAsync(Announcement announcement, string userId)
    {
        announcement.IsDeleted = !announcement.IsDeleted;
        announcement.LastUpdatedById = userId;
        announcement.LastUpdatedOn = DateTime.UtcNow;

        _unitOfWork.Announcements.Update(announcement);
        await _unitOfWork.CompleteAsync();

        return new Response<Announcement> { IsSuccess = true, Model = announcement };
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync()
    {
        var announcementQueryable = _unitOfWork.Announcements.GetQueryable();

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

        var announcements = await announcementQueryable.ToListAsync();

        return announcements;
    }
}
