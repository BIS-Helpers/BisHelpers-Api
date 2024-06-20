using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.Domain.Dtos.Announcement;
public class AnnouncementListDto
{
    public IEnumerable<AnnouncementBaseDto> Announcements { get; set; } = [];

    public IEnumerable<AcademicCourseBaseDto> AcademicCourses { get; set; } = [];
}
