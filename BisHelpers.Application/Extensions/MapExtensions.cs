namespace BisHelpers.Application.Extensions;
public static class MapExtensions
{
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

    public static ProfileDto MapToProfileDto(this AppUser model)
    {
        var profile = new ProfileDto
        {
            FullName = model.FullName,
            Email = model.Email ?? string.Empty,
            PhoneNumber = model.PhoneNumber ?? string.Empty,
            Gender = model.Gender,
            BirthDate = model.BirthDate,
        };

        return profile;
    }
}
