namespace BisHelpers.Application.Extensions.MappingExtensions;

public static class ProfessorMappingExtension
{
    public static IEnumerable<ProfessorWithLecturesDto> ToProfessorWithLecturesDto(this IEnumerable<Professor> modelList, bool isDetailed = false)
    {
        var modelListDto = modelList.Select(m => new ProfessorWithLecturesDto
        {
            Id = m.Id,
            FullName = m.FullName,
            AcademicLectures = m.AcademicCourses
            .SelectMany(a => a.AcademicLectures)
            .ToList().MapToDto(),

            LastUpdatedBy = isDetailed ? m.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? m.LastUpdatedOn : null,
            CreatedBy = isDetailed ? m.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? m.CreatedOn : null,
            IsDeleted = isDetailed ? m.IsDeleted : null,
        });

        return modelListDto;
    }

    public static IEnumerable<ProfessorBaseDto> ToProfessorBaseDto(this IEnumerable<Professor> modelList, bool isDetailed = false)
    {
        var modelListDto = modelList.Select(m => new ProfessorBaseDto
        {
            Id = m.Id,
            FullName = m.FullName,

            LastUpdatedBy = isDetailed ? m.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? m.LastUpdatedOn : null,
            CreatedBy = isDetailed ? m.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? m.CreatedOn : null,
            IsDeleted = isDetailed ? m.IsDeleted : null,
        });

        return modelListDto;
    }

    public static ProfessorWithLecturesDto ToProfessorWithLecturesDto(this Professor model, bool isDetailed = false)
    {
        var modelDto = new ProfessorWithLecturesDto
        {
            Id = model.Id,
            FullName = model.FullName,
            AcademicLectures = model.AcademicCourses
            .SelectMany(a => a.AcademicLectures)
            .ToList().MapToDto(),

            LastUpdatedBy = isDetailed ? model.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? model.LastUpdatedOn : null,
            CreatedBy = isDetailed ? model.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? model.CreatedOn : null,
            IsDeleted = isDetailed ? model.IsDeleted : null,
        };

        return modelDto;
    }

    public static ProfessorBaseDto ToProfessorBaseDto(this Professor model, bool isDetailed = false)
    {
        var modelDto = new ProfessorBaseDto
        {
            Id = model.Id,
            FullName = model.FullName,

            LastUpdatedBy = isDetailed ? model.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? model.LastUpdatedOn : null,
            CreatedBy = isDetailed ? model.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? model.CreatedOn : null,
            IsDeleted = isDetailed ? model.IsDeleted : null,
        };

        return modelDto;
    }

    public static Professor ToProfessor(this ProfessorCreateDto dto)
    {
        var professor = new Professor
        {
            FullName = dto.FullName,
        };

        return professor;
    }
}
