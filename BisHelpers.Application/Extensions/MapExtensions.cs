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
            RegisteredAcademicLectures = model.Student?.AcademicLectures?
                .Select(a => a.AcademicLecture ?? new AcademicLecture())
                .ToAcademicLectureWithProfessorDto()
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
