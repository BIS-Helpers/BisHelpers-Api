namespace BisHelpers.Domain.Dtos.Announcement;
public class AnnouncementUpdateDto
{
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? AcademicLectureId { get; set; }
}
