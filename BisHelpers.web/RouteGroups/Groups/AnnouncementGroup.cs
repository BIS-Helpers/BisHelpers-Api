using BisHelpers.Application.Services.AnnouncementService;
using BisHelpers.Application.Services.StudentService;
using BisHelpers.Domain.Dtos.Announcement;

namespace BisHelpers.web.RouteGroups.Groups;

public static class AnnouncementGroup
{
    public static RouteGroupBuilder GroupAnnouncementVersionOne(this RouteGroupBuilder builder)
    {
        builder.MapGet("/StudentAnnouncement", [Authorize(Roles = AppRoles.Student)]
        async (IAnnouncementService announcementService, IStudentService studentService, HttpContext context) =>
        {
            var studentUser = await studentService.GetDetailedStudentUserByUserIdAsync(context.User.GetUserId());

            if (studentUser is null || studentUser.Student is null)
                return Results.NotFound();

            var announcements = await announcementService.GetAllForStudentAsync(studentUser.Student);

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
