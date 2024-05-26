using BisHelpers.Application.Services.AcademicCourse;
using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.web.RouteGroups.Groups;

public static class AcademicCourseGroup
{
    public static RouteGroupBuilder GroupAcademicCourseVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/ProfessorWithLectures", [Authorize(Roles = AppRoles.Admin)]
        async ([FromBody] ProfessorAcademicCourseDto dto, IValidator<ProfessorAcademicCourseDto> validator, IAcademicCourseService academicCourseService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var addResponse = await academicCourseService.AddProfessorAsync(dto, context.User.GetUserId());

            if (!addResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [addResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Created();
        })
        .EndPointConfigurations(Name: "Add Professor To Course", version: Versions.Version1)
        .CreatedResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .UnauthorizedResponseConfiguration();

        return builder;
    }
}
