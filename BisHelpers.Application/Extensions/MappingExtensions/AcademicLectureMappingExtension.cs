using BisHelpers.Domain.Dtos.AcademicLecture;

namespace BisHelpers.Application.Extensions.MappingExtensions;
public static class AcademicLectureMappingExtension
{
    public static IEnumerable<AcademicLecture> MapToModel(this IEnumerable<CreateAcademicLectureDto> DtoList)
    {
        var modelList = DtoList.Select(d => new AcademicLecture
        {
            Day = d.Day,
            GroupNumber = d.GroupNumber,
            StartTime = d.StartTime,
        });

        return modelList;
    }

    public static IEnumerable<AcademicLectureBaseDto> ToAcademicLectureBaseDto(this IEnumerable<AcademicLecture> DtoList, bool isDetailed = false)
    {
        var modelList = DtoList.Select(d => new AcademicLectureBaseDto
        {
            Id = d.Id,
            StartTime = d.StartTime,
            Day = d.Day,
            GroupNumber = d.GroupNumber,

            LastUpdatedBy = isDetailed ? d.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? d.LastUpdatedOn : null,
            CreatedBy = isDetailed ? d.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? d.CreatedOn : null,
            IsDeleted = isDetailed ? d.IsDeleted : null,

        });

        return modelList;
    }

    public static IEnumerable<AcademicLectureWithProfessorAndCourseDto> ToAcademicLectureWithProfessorDto(this IEnumerable<AcademicLecture> DtoList, bool isDetailed = false)
    {
        var modelList = DtoList.Select(d => new AcademicLectureWithProfessorAndCourseDto
        {
            Id = d.Id,
            Semester = d.ProfessorAcademicCourse?.AcademicSemester?.Semester?.Name ?? string.Empty,
            Year = d.ProfessorAcademicCourse?.AcademicSemester?.EndDate.Year.GetAcademicYear() ?? string.Empty,
            StartTime = d.StartTime,
            Day = d.Day,
            GroupNumber = d.GroupNumber,
            Professor = d.ProfessorAcademicCourse?.Professor?.ToProfessorBaseDto(isDetailed),
            AcademicCourse = d.ProfessorAcademicCourse?.AcademicCourses?.MapToDto(isDetailed),
            LastUpdatedBy = isDetailed ? d.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? d.LastUpdatedOn : null,
            CreatedBy = isDetailed ? d.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? d.CreatedOn : null,
            IsDeleted = isDetailed ? d.IsDeleted : null,

        });

        return modelList;
    }

    public static AcademicLectureWithProfessorAndCourseDto ToAcademicLectureWithProfessorAndCourseDto(this AcademicLecture model, bool withBaseDto = false)
    {
        var modelDto = new AcademicLectureWithProfessorAndCourseDto
        {
            Id = model.Id,
            Semester = model.ProfessorAcademicCourse?.AcademicSemester?.Semester?.Name ?? string.Empty,
            Year = model.ProfessorAcademicCourse?.AcademicSemester?.EndDate.Year.GetAcademicYear() ?? string.Empty,
            StartTime = model.StartTime,
            Day = model.Day,
            GroupNumber = model.GroupNumber,
            Professor = model.ProfessorAcademicCourse?.Professor?.ToProfessorBaseDto(withBaseDto),
            AcademicCourse = model.ProfessorAcademicCourse?.AcademicCourses?.MapToDto(withBaseDto),

            LastUpdatedBy = withBaseDto ? model.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = withBaseDto ? model.LastUpdatedOn.AsUtcTime() : null,
            CreatedBy = withBaseDto ? model.CreatedBy?.FullName : null,
            CreatedOn = withBaseDto ? model.CreatedOn.AsUtcTime() : null,
            IsDeleted = withBaseDto ? model.IsDeleted : null,
        };

        return modelDto;
    }
}
