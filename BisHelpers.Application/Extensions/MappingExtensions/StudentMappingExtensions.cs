using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.Application.Extensions.MappingExtensions;
public static class StudentMappingExtensions
{
    public static Student MapToStudent(this RegisterDto model)
    {
        var student = new Student
        {
            CollegeId = model.CollegeId,
            DateOfJoin = model.DateOfJoin,
        };

        return student;
    }

    public static IEnumerable<StudentBaseDto> ToStudentBaseDto(this IEnumerable<AppUser> modelList, bool withBaseDto = false)
    {
        var modelListDto = modelList.Select(m => new StudentBaseDto
        {
            UserId = m.Id,
            StudentId = m.Student?.Id ?? 0,
            Level = m.Student?.DateOfJoin.ToCollegeLevel() ?? string.Empty,
            CollegeId = m.Student?.CollegeId ?? string.Empty,
            FullName = m.FullName,
            Email = m.Email ?? string.Empty,
            PhoneNumber = m.PhoneNumber ?? string.Empty,
            Gender = m.Gender,
            BirthDate = m.BirthDate,
            Gpa = m.Student?.Registrations.FirstOrDefault()?.Gpa ?? 0,
            TotalEarnedHours = m.Student?.Registrations.FirstOrDefault()?.TotalEarnedHours ?? 0,

            LastUpdatedBy = withBaseDto ? m.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = withBaseDto ? m.LastUpdatedOn : null,
            CreatedBy = withBaseDto ? m.CreatedBy?.FullName : null,
            CreatedOn = withBaseDto ? m.CreatedOn : null,
            IsDeleted = withBaseDto ? m.IsDeleted : null,
        });

        return modelListDto;
    }
    public static StudentDetailedDto ToStudentDetailedDto(this AppUser model, bool withBaseDto = false)
    {
        var modelDto = new StudentDetailedDto
        {
            UserId = model.Id,
            StudentId = model.Student?.Id ?? 0,
            Level = model.Student?.DateOfJoin.ToCollegeLevel() ?? string.Empty,
            CollegeId = model.Student?.CollegeId ?? string.Empty,
            FullName = model.FullName,
            Email = model.Email ?? string.Empty,
            PhoneNumber = model.PhoneNumber ?? string.Empty,
            Gender = model.Gender,
            BirthDate = model.BirthDate,
            Gpa = model.Student?.Registrations.FirstOrDefault()?.Gpa ?? 0,
            TotalEarnedHours = model.Student?.Registrations.FirstOrDefault()?.TotalEarnedHours ?? 0,
            RegisteredAcademicLectures = model.Student?.Registrations?
                .SelectMany(r => r.Lectures.Select(l => l.AcademicLecture ?? new AcademicLecture()))
                .ToAcademicLectureWithProfessorDto() ?? [],

            LastUpdatedBy = withBaseDto ? model.LastUpdatedBy?.FullName : null,
            LastUpdatedOn = withBaseDto ? model.LastUpdatedOn : null,
            CreatedBy = withBaseDto ? model.CreatedBy?.FullName : null,
            CreatedOn = withBaseDto ? model.CreatedOn : null,
            IsDeleted = withBaseDto ? model.IsDeleted : null,
        };

        return modelDto;
    }

}
