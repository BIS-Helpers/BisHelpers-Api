namespace BisHelpers.web.RouteGroups.VersionOne;

public static class ProfessorGroup
{
    public static RouteGroupBuilder GroupProfessorVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/", [Authorize(Roles = AppRoles.Admin)] async ([FromBody] ProfessorCreateDto dto, IValidator<ProfessorCreateDto> validator, IProfessorService professorService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var AddResponse = await professorService.Add(dto, context.User.GetUserId());

            if (!AddResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [AddResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Created();
        })
        .EndPointConfigurations(Name: "Add Professor", version: Versions.Version1)
        .CreatedResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest);

        return builder;
    }
}
