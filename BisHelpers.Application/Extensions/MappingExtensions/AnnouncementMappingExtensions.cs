using BisHelpers.Domain.Dtos.Announcement;

namespace BisHelpers.Application.Extensions.MappingExtensions;
public static class AnnouncementMappingExtensions
{
    public static IEnumerable<AnnouncementBaseDto> ToAnnouncementBaseDto(this IEnumerable<Announcement> modelList, bool withBaseDto = false)
    {
        var modelListDto = modelList.Select(m => new AnnouncementBaseDto
        {
            Id = m.Id,
            Title = m.Title,
            Content = m.Content,
            AcademicLecture = m.AcademicLecture?.ToAcademicLectureWithProfessorAndCourseDto() ?? new(),

            LastUpdatedBy = withBaseDto ? m.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = withBaseDto ? m.LastUpdatedOn.AsUtcTime() : null,
            CreatedBy = withBaseDto ? m.CreatedBy?.FullName : null,
            CreatedOn = withBaseDto ? m.CreatedOn.AsUtcTime() : null,
            IsDeleted = withBaseDto ? m.IsDeleted : null,
        });

        return modelListDto;
    }
}
