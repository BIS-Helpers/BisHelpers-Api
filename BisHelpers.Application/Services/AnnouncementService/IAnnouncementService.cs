namespace BisHelpers.Application.Services.AnnouncementService;
public interface IAnnouncementService
{
    public Task<IEnumerable<Announcement>> GetAllForStudentAsync(Student student);

}
