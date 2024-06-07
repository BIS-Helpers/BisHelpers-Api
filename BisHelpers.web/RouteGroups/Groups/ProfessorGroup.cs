namespace BisHelpers.web.RouteGroups.Groups;

public static class ProfessorGroup
{
    public static RouteGroupBuilder GroupProfessorVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/", [Authorize(Roles = AppRoles.Admin)]
        async ([FromBody] ProfessorCreateDto dto, IValidator<ProfessorCreateDto> validator, IProfessorService professorService, HttpContext context) =>
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

            var createdProfessor = AddResponse.Model!.ToProfessorBaseDto(true);
            createdProfessor.CreatedBy = context.User.GetFullName();

            return Results.Created(string.Empty, createdProfessor);
        })
        .EndPointConfigurations(Name: "Add Professor", version: Versions.Version1)
        .CreatedResponseConfiguration<ProfessorBaseDto>()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/", [Authorize(Roles = AppRoles.Admin)]
        async (IProfessorService professorService, HttpContext context) =>
        {
            var professors = await professorService.GetAllAsync();

            var professorsDto = professors.ToProfessorBaseDto(true);

            return Results.Ok(professorsDto);
        })
        .EndPointConfigurations(Name: "Get All Professors", version: Versions.Version1)
        .OkResponseConfiguration<IEnumerable<ProfessorBaseDto>>()
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/{id}", [Authorize(Roles = AppRoles.Admin)]
        async (int id, IProfessorService professorService, HttpContext context) =>
        {
            var professor = await professorService.GetById(id);

            if (professor is null)
                return Results.NotFound();

            var professorDto = professor.ToProfessorWithLecturesDto();

            return Results.Ok(professorDto);
        })
        .EndPointConfigurations(Name: "Get Professor By Id", version: Versions.Version1)
        .OkResponseConfiguration<ProfessorWithLecturesDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        return builder;
    }
}
