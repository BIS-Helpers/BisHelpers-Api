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

        builder.MapPut("/{id}", [Authorize(Roles = AppRoles.Admin)]
        async (int id, [FromBody] ProfessorUpdateDto dto, IValidator<ProfessorUpdateDto> validator, IProfessorService professorService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var professor = await professorService.GetById(id);

            if (professor is null)
                return Results.NotFound();

            var updateResponse = await professorService.UpdateAsync(dto, professor, context.User.GetUserId());

            if (!updateResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [updateResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Ok(updateResponse.Model?.ToProfessorBaseDto(true));
        })
        .EndPointConfigurations(Name: "Update Professor", version: Versions.Version1)
        .OkResponseConfiguration<ProfessorBaseDto>()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapPatch("/{id}", [Authorize(Roles = AppRoles.Admin)]
        async (int id, IProfessorService professorService, HttpContext context) =>
        {
            var professor = await professorService.GetById(id);

            if (professor is null)
                return Results.NotFound();

            var updateResponse = await professorService.ToggleStatusAsync(professor, context.User.GetUserId());

            if (!updateResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [updateResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.NoContent();
        })
        .EndPointConfigurations(Name: "Toggle Status of Professor", version: Versions.Version1)
        .NoContentResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
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
            var professor = await professorService.GetByIdWithAcademicCoursesAndAcademicLecturesAsync(id);

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
