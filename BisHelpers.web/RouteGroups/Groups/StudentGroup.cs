using BisHelpers.Application.Services.StudentService;
using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.web.RouteGroups.Groups;

public static class StudentGroup
{
    public static RouteGroupBuilder GroupStudentVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/RegisterAcademicLectures", [Authorize(Roles = AppRoles.Student)]
        async ([FromBody] RegisterAcademicLecturesDto dto, IStudentService studentService, HttpContext context) =>
        {
            var result = await studentService.RegisterAcademicLecturesAsync(context.User.GetUserId(), dto);

            if (!result.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [result.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Created();
        })
        .EndPointConfigurations(Name: "Register Academic Lectures", version: Versions.Version1)
        .CreatedResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/GpaAnalysis", [Authorize(Roles = AppRoles.Student)]
        async (IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(context.User.GetUserId());

            if (studentUser is null)
                return Results.NotFound();

            var gpaAnalysisDto = studentUser.ToGpaAnalysisDto();

            gpaAnalysisDto.Level = studentUser.Student?.DateOfJoin.ToCollegeLevel() ?? string.Empty;

            return Results.Ok(gpaAnalysisDto);
        })
        .EndPointConfigurations(Name: "Get GPA Analysis Report", version: Versions.Version1)
        .OkResponseConfiguration<GpaAnalysisDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, false)
        .UnauthorizedResponseConfiguration();

        return builder;
    }
}
