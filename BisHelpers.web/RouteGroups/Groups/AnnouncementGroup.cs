using BisHelpers.Application.Services.AnnouncementService;
using BisHelpers.Application.Services.StudentService;
using BisHelpers.Domain.Dtos.Announcement;

namespace BisHelpers.web.RouteGroups.Groups;

public static class AnnouncementGroup
{
    public static RouteGroupBuilder GroupAnnouncementVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapPost("/", [Authorize(Roles = AppRoles.Admin)]
        async ([FromBody] AnnouncementCreateDto dto, IValidator<AnnouncementCreateDto> validator, IAnnouncementService announcementService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var AddResponse = await announcementService.AddAsync(dto, context.User.GetUserId());

            if (!AddResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [AddResponse.ErrorBody],
                    StatusCode = 400,
                });

            var createdProfessor = AddResponse.Model!.ToAnnouncementBaseDto(true);
            createdProfessor.CreatedBy = context.User.GetFullName();

            return Results.Created(string.Empty, createdProfessor);
        })
        .EndPointConfigurations(Name: "Add Announcement", version: Versions.Version1)
        .CreatedResponseConfiguration<AnnouncementBaseDto>()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .UnauthorizedResponseConfiguration();

        builder.MapPut("/{id}", [Authorize(Roles = AppRoles.Admin)]
        async (int id, [FromBody] AnnouncementUpdateDto dto, IValidator<AnnouncementUpdateDto> validator, IAnnouncementService announcementService, HttpContext context) =>
        {
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
                return Results.BadRequest(new ErrorDto(context)
                {
                    StatusCode = 400,
                    Errors = validationResult.ToErrorList(),
                });

            var professor = await announcementService.GetByIdAsync(id);

            if (professor is null)
                return Results.NotFound();

            var updateResponse = await announcementService.UpdateAsync(dto, professor, context.User.GetUserId());

            if (!updateResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [updateResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.Ok(updateResponse.Model?.ToAnnouncementBaseDto(true));
        })
        .EndPointConfigurations(Name: "Update Announcement", version: Versions.Version1)
        .OkResponseConfiguration<AnnouncementBaseDto>()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapDelete("/{id}", [Authorize(Roles = AppRoles.Admin)]
        async (int id, IAnnouncementService announcementService, HttpContext context) =>
        {
            var announcement = await announcementService.GetByIdAsync(id);

            if (announcement is null)
                return Results.NotFound();

            var updateResponse = await announcementService.DeleteAsync(announcement);

            if (!updateResponse.IsSuccess)
                return Results.BadRequest(new ErrorDto(context)
                {
                    Errors = [updateResponse.ErrorBody],
                    StatusCode = 400,
                });

            return Results.NoContent();
        })
        .EndPointConfigurations(Name: "Delete Announcement", version: Versions.Version1)
        .NoContentResponseConfiguration()
        .ErrorResponseConfiguration(StatusCodes.Status400BadRequest)
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, withBody: false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/Student", [Authorize(Roles = AppRoles.Student)]
        async (IAnnouncementService announcementService, IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(context.User.GetUserId());

            if (studentUser is null || studentUser.Student is null)
                return Results.NotFound();

            var announcements = await announcementService.GetAllAsync(studentUser.Student);

            if (announcements is null)
                return Results.NotFound();

            var dtoList = announcements.ToAnnouncementListDto();

            return Results.Ok(dtoList);
        })
        .EndPointConfigurations(Name: "Get All Announcements For Student", version: Versions.Version1)
        .OkResponseConfiguration<AnnouncementListDto>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, false)
        .UnauthorizedResponseConfiguration();

        builder.MapGet("/", [Authorize(Roles = AppRoles.Admin)]
        async (IAnnouncementService announcementService, IStudentService studentService, HttpContext context) =>
        {
            var announcements = await announcementService.GetAllAsync();

            if (announcements is null)
                return Results.NotFound();

            var dtoList = announcements.ToAnnouncementBaseDto();

            return Results.Ok(dtoList);
        })
        .EndPointConfigurations(Name: "Get All Announcements", version: Versions.Version1)
        .OkResponseConfiguration<IEnumerable<AnnouncementBaseDto>>()
        .ErrorResponseConfiguration(StatusCodes.Status404NotFound, false)
        .UnauthorizedResponseConfiguration();

        return builder;
    }
}
