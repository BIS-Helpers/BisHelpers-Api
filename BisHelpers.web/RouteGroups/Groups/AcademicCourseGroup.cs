namespace BisHelpers.web.RouteGroups.Groups;

public static class AcademicCourseGroup
{
    public static RouteGroupBuilder GroupAcademicCourseVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/AddProfessor", [Authorize(Roles = AppRoles.Admin)] async ([FromBody] ProfessorCreateDto dto, IValidator<ProfessorCreateDto> validator, IProfessorService professorService, HttpContext context) =>
        {

        });

        return builder;
    }
}
