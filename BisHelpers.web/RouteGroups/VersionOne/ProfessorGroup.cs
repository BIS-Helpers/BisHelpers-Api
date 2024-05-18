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

            var AddResponse = await professorService.AddAsync(dto, context.User.GetUserId());

            if (!AddResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [AddResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.CreatedAtRoute(string.Join(' ', "Get Professor By Id", Versions.Version1), new { AddResponse.Model!.Id }, AddResponse.Model.MapToDto());
        })
        .EndPointConfigurations(Name: "Add Professor", version: Versions.Version1)
        .CreatedResponseConfiguration<ProfessorDto>()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/", [Authorize(Roles = AppRoles.Admin)] async (IProfessorService professorService, HttpContext context) =>
        {
            var professors = await professorService.GetAllAsync();
            return Results.Ok(professors);
        })
        .EndPointConfigurations(Name: "Get All Professors", version: Versions.Version1)
        .OkResponseConfiguration<IEnumerable<ProfessorDto>>()
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/{id}", [Authorize(Roles = AppRoles.Admin)] async (int id, IProfessorService professorService, HttpContext context) =>
        {
            var professor = await professorService.GetById(id);

            if (professor is null)
                return Results.NotFound();

            return Results.Ok(professor);
        })
        .EndPointConfigurations(Name: "Get Professor By Id", version: Versions.Version1)
        .OkResponseConfiguration<ProfessorDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        return builder;
    }
}
