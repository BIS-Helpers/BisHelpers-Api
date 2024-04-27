namespace BisHelpers.Application.Extensions;
public static class MapExtensions
{
    public static AppUser MapToAppUser(this RegisterDto model)
    {
        var user = new AppUser
        {
            Email = model.Email,
            FullName = model.FullName,
            BirthDate = model.BirthDate,
            PhoneNumber = model.PhoneNumber,
            Gender = model.Gender,
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
}
