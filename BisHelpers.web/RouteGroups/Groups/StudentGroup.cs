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
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(context.User.GetUserId());

            if (studentUser is null || studentUser.Student is null)
                return Results.NotFound();

            var result = await studentService.RegisterAcademicLecturesAsync(studentUser.Student, dto);

            if (!result.IsSuccess)
                return Results.BadRequest(new ErrorDto(context) { Errors = [result.ErrorBody], StatusCode = 400 });

            return Results.Created();
        })
        .EndPointConfigurations(Name: "Register Academic Lectures", version: Versions.Version1)
        .CreatedResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapDelete("/DropActiveRegistration/{studentUserId}", [Authorize]
        async (string studentUserId, IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(studentUserId);

            if (studentUser is null || studentUser.Student is null)
                return Results.NotFound();

            var result = await studentService.DropActiveRegistrationAsync(studentUser.Student);

            if (!result.IsSuccess)
                return Results.BadRequest(new ErrorDto(context) { Errors = [result.ErrorBody], StatusCode = 400 });

            return Results.NoContent();
        })
        .EndPointConfigurations(Name: "Drop Active Registration", version: Versions.Version1)
        .NoContentResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/GpaAnalysis", [Authorize(Roles = AppRoles.Student)]
        async (IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(context.User.GetUserId());

            if (studentUser is null || studentUser.Student is null)
                return Results.NotFound();

            var result = await studentService.IsStudentHasActiveRegistrationAsync(studentUser.Student);

            if (!result)
                return Results.NotFound();

            var gpaAnalysisDto = studentUser.ToGpaAnalysisDto();

            gpaAnalysisDto.Level = studentUser.Student?.DateOfJoin.ToCollegeLevel() ?? string.Empty;

            return Results.Ok(gpaAnalysisDto);
        })
        .EndPointConfigurations(Name: "Get GPA Analysis Report", version: Versions.Version1)
        .OkResponseConfiguration<GpaAnalysisDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/{studentUserId}", [Authorize(Roles = AppRoles.Admin)]
        async (string studentUserId, IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(studentUserId, true);

            if (studentUser is null || studentUser.Student is null)
                return Results.NotFound();

            var studentDto = studentUser.ToStudentDetailedDto(true);

            return Results.Ok(studentDto);
        })
        .EndPointConfigurations(Name: "Get Student By Id", version: Versions.Version1)
        .OkResponseConfiguration<StudentDetailedDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/", [Authorize(Roles = AppRoles.Admin)]
        async (IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetAllAsync();

            var professorsDto = studentUser.ToStudentBaseDto(true);

            return Results.Ok(professorsDto);
        })
        .EndPointConfigurations(Name: "Get All Student", version: Versions.Version1)
        .OkResponseConfiguration<IEnumerable<StudentBaseDto>>()
        .UnauthorizedResponseConfiguration();


        return builder;
    }
}
