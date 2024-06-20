namespace BisHelpers.Domain.Entities;
public class Announcement : BaseEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public AcademicLecture? AcademicLecture { get; set; }
    public int? AcademicLectureId { get; set; }
}
