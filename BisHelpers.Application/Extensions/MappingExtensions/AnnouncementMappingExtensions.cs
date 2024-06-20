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
            AcademicLecture = m.AcademicLecture is not null ? m.AcademicLecture.ToAcademicLectureWithProfessorAndCourseDto() : new()
            {
                AcademicCourse = new AcademicCourseBaseDto { Name = "BIS Family" }
            },

            LastUpdatedBy = withBaseDto ? m.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = m.LastUpdatedOn.AsUtcTime(),
            CreatedBy = withBaseDto ? m.CreatedBy?.FullName : null,
            CreatedOn = m.CreatedOn.AsUtcTime(),
            IsDeleted = withBaseDto ? m.IsDeleted : null,
        });

        return modelListDto;
    }

    public static AnnouncementBaseDto ToAnnouncementBaseDto(this Announcement model, bool withBaseDto = false)
    {
        var dto = new AnnouncementBaseDto
        {
            Id = model.Id,
            Content = model.Content,
            Title = model.Title,
            AcademicLecture = model.AcademicLecture?.ToAcademicLectureWithProfessorAndCourseDto(),

            LastUpdatedBy = withBaseDto ? model.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = model.LastUpdatedOn.AsUtcTime(),
            CreatedBy = withBaseDto ? model.CreatedBy?.FullName : null,
            CreatedOn = model.CreatedOn.AsUtcTime(),
            IsDeleted = withBaseDto ? model.IsDeleted : null,
        };

        return dto;
    }

    public static AnnouncementListDto ToAnnouncementListDto(this IEnumerable<Announcement> modelList, bool withBaseDto = false)
    {
        var dto = new AnnouncementListDto
        {
            Announcements = modelList.ToAnnouncementBaseDto(true),
            AcademicCourses = modelList.Where(m => m.AcademicLecture is not null)
                .Select(m => m.AcademicLecture?.ProfessorAcademicCourse?.AcademicCourses ?? new()).DistinctBy(c => c.Id).MapToDto() ?? [],
        };

        return dto;
    }

    public static Announcement ToAnnouncement(this AnnouncementCreateDto dto)
    {
        var model = new Announcement
        {
            AcademicLectureId = dto.AcademicLectureId,
            Title = dto.Title,
            Content = dto.Content,
        };

        return model;
    }

    public static Announcement ToAnnouncement(this AnnouncementUpdateDto dto, Announcement announcement)
    {
        announcement.Content = dto.Content;
        announcement.Title = dto.Title;
        announcement.AcademicLectureId = dto.AcademicLectureId;

        return announcement;
    }

}
