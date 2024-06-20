using BisHelpers.Domain.Dtos.Announcement;

namespace BisHelpers.Application.Services.AnnouncementService;
public interface IAnnouncementService
{
    public Task<IEnumerable<Announcement>> GetAllAsync(Student student);
    public Task<IEnumerable<Announcement>> GetAllAsync();
    public Task<Announcement?> GetByIdAsync(int id);
    public Task<Response<Announcement>> AddAsync(AnnouncementCreateDto dto, string userId);
    public Task<Response<Announcement>> UpdateAsync(AnnouncementUpdateDto dto, Announcement announcement, string userId);
    public Task<Response<Announcement>> ToggleStatusAsync(Announcement announcement, string userId);
}
