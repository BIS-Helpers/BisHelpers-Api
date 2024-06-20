namespace BisHelpers.Domain.Dtos.Announcement;
public class AnnouncementBaseDto : BaseDto
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public AcademicLectureWithProfessorAndCourseDto? AcademicLecture { get; set; }
}
