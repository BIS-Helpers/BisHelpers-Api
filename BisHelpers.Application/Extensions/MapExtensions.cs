using BisHelpers.Application.Extensions.MappingExtensions;
using BisHelpers.Domain.Entities.RelatedData;

namespace BisHelpers.Application.Extensions;
public static class MapExtensions
{
    public static ProfileDto MapToProfileDto(this AppUser model)
    {
        var profile = new ProfileDto
        {
            FullName = model.FullName,
            Email = model.Email ?? string.Empty,
            PhoneNumber = model.PhoneNumber ?? string.Empty,
            Gender = model.Gender,
            BirthDate = model.BirthDate,
            RegisteredAcademicLectures = model.Student?.AcademicLectures?.Select(a => a.AcademicLecture).ToList()!.MapToDto()
        };

        return profile;
    }

    public static AppUser MapToAppUser(this RegisterDto model)
    {
        var user = new AppUser
        {
            Email = model.Email,
            UserName = model.Email,
            FullName = model.FullName,
            BirthDate = model.BirthDate,
            PhoneNumber = model.PhoneNumber,
            Gender = model.Gender.ToLower(),
        };

        return user;
    }

    public static Student MapToStudent(this RegisterDto model)
    {
        var student = new Student
        {
            CollegeId = model.CollegeId,
            DateOfJoin = model.DateOfJoin,
        };

        return student;
    }

    public static AcademicCourseDetailedDto MapToDto(this AcademicCourse model, bool isDetailed = false)
    {
        var modelDto = new AcademicCourseDetailedDto
        {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            CreditHours = model.CreditHours,
            Professors = model.Professors.ToList().MapToDto(isDetailed),
        };

        return modelDto;
    }

    public static ProfessorAcademicCourseDto MapToDto(this ProfessorAcademicCourse model)
    {
        var modelDto = new ProfessorAcademicCourseDto
        {
            AcademicSemester = model.AcademicSemester?.Semester?.Name,
            AcademicYear = model.AcademicSemester?.endDate.Year.GetAcademicYear(),
            Professor = model.Professor?.ToProfessorWithLecturesDto(),
            AcademicCourse = model.AcademicCourses?.Name
        };

        return modelDto;
    }


    public static List<ProfessorAcademicCourseDto> MapToDto(this List<ProfessorAcademicCourse> DtoList, bool isDetailed = false)
    {
        var modelList = DtoList.Select(d => new ProfessorAcademicCourseDto
        {
            AcademicSemester = d.AcademicSemester?.Semester?.Name,
            AcademicYear = d.AcademicSemester?.endDate.Year.GetAcademicYear(),
            Professor = d.Professor?.ToProfessorWithLecturesDto(),

            LastUpdatedBy = isDetailed ? d.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? d.LastUpdatedOn : null,
            CreatedBy = isDetailed ? d.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? d.CreatedOn : null,
            IsDeleted = isDetailed ? d.IsDeleted : null,

        }).ToList();

        return modelList;
    }


    public static List<AcademicLectureDto> MapToDto(this List<AcademicLecture> DtoList, bool isDetailed = false)
    {
        var modelList = DtoList.Select(d => new AcademicLectureDto
        {
            Id = d.Id,
            StartTime = d.StartTime,
            Day = d.Day,
            GroupNumber = d.GroupNumber,
            ProfessorAcademicCourse = d.ProfessorAcademicCourse?.MapToDto(),

            LastUpdatedBy = isDetailed ? d.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = isDetailed ? d.LastUpdatedOn : null,
            CreatedBy = isDetailed ? d.CreatedBy?.FullName : null,
            CreatedOn = isDetailed ? d.CreatedOn : null,
            IsDeleted = isDetailed ? d.IsDeleted : null,

        }).ToList();

        return modelList;
    }

    public static IEnumerable<AcademicCourseDto> MapToDto(this IEnumerable<AcademicCourse> modelList)
    {
        var modelListDto = modelList.Select(m => new AcademicCourseDto
        {
            Id = m.Id,
            Code = m.Code,
            CreditHours = m.CreditHours,
            Name = m.Name,
        }).ToList();

        return modelListDto;
    }

    public static ProfessorAcademicCourse MapToModel(this CreateProfessorAcademicCourseDto dto)
    {
        var model = new ProfessorAcademicCourse
        {
            ProfessorId = dto.ProfessorId,
            AcademicCourseId = dto.AcademicCourseId,
            AcademicLectures = dto.Lectures.MapToModel()
        };

        return model;
    }

    public static List<AcademicLecture> MapToModel(this List<CreateAcademicLectureDto> DtoList)
    {
        var modelList = DtoList.Select(d => new AcademicLecture
        {
            Day = d.Day,
            GroupNumber = d.GroupNumber,
            StartTime = d.StartTime,
        }).ToList();

        return modelList;
    }
}
