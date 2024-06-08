using BisHelpers.Domain.Dtos.Student;

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
            Gpa = model.Student?.Registrations.FirstOrDefault()?.Gpa,
            TotalEarnedHours = model.Student?.Registrations.FirstOrDefault()?.TotalEarnedHours,
            RegisteredAcademicLectures = model.Student?.Registrations?
                .SelectMany(r => r.Lectures.Select(l => l.AcademicLecture ?? new AcademicLecture()))
                .ToAcademicLectureWithProfessorDto()
        };

        return profile;
    }

    public static GpaAnalysisDto ToGpaAnalysisDto(this AppUser model)
    {
        var dto = new GpaAnalysisDto
        {
            FullName = model.FullName,
            Gpa = model.Student?.Registrations.FirstOrDefault()?.Gpa ?? 0,
            TotalEarnedHours = model.Student?.Registrations.FirstOrDefault()?.TotalEarnedHours ?? 0,
            CollegeId = model.Student?.CollegeId ?? string.Empty,
            RegisteredAcademicLectures = model.Student?.Registrations?
                .SelectMany(r => r.Lectures.Select(l => l.AcademicLecture ?? new AcademicLecture()))
                .ToAcademicLectureWithProfessorDto()
        };

        dto.MinGradeToSaveGpa = model.Student?.Registrations?
                .SelectMany(r => r.Lectures.Select(l => l.AcademicLecture?.ProfessorAcademicCourse?.AcademicCourses ?? new AcademicCourse()))
                .GetMinGrade(dto.MinPointsToSaveGpa) ?? string.Empty;

        return dto;
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

    #region AcademicCourse
    public static AcademicCourseWithProfessorsDto MapToDto(this AcademicCourse model, bool isDetailed = false)
    {
        var modelDto = new AcademicCourseWithProfessorsDto
        {
            Id = model.Id,
            Name = model.Name,
            Code = model.Code,
            CreditHours = model.CreditHours,
            Professors = model.Professors.Select(p => p.Professor ?? new Professor())
                .ToProfessorWithLecturesDto().ToList(),
        };

        return modelDto;
    }

    public static IEnumerable<AcademicCourseBaseDto> MapToDto(this IEnumerable<AcademicCourse> modelList)
    {
        var modelListDto = modelList.Select(m => new AcademicCourseBaseDto
        {
            Id = m.Id,
            Code = m.Code,
            CreditHours = m.CreditHours,
            Name = m.Name,
        }).ToList();

        return modelListDto;
    }

    public static ProfessorAcademicCourse MapToModel(this AddProfessorToAcademicCourseDto dto)
    {
        var model = new ProfessorAcademicCourse
        {
            ProfessorId = dto.ProfessorId,
            AcademicCourseId = dto.AcademicCourseId,
            AcademicLectures = dto.Lectures.MapToModel().ToList(),
        };

        return model;
    }
    #endregion
}
