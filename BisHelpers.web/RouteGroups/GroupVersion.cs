using BisHelpers.web.RouteGroups.Groups;

namespace BisHelpers.web.RouteGroups;

public static class GroupVersion
{
    public static RouteGroupBuilder VersionOneGroup(this RouteGroupBuilder builder)
    {
        builder.MapGroup("/auth").GroupAuthVersionOne().WithTags("Authentication");
        builder.MapGroup("/professor").GroupProfessorVersionOne().WithTags("Professor");
        builder.MapGroup("/AcademicCourse").GroupAcademicCourseVersionOne().WithTags("Academic Course");

        return builder;
    }

    public static RouteGroupBuilder VersionTwoGroup(this RouteGroupBuilder builder)
    {
        builder.MapGroup("/Auth").GroupAuthVersionTwo().WithTags("Authentication");

        return builder;
    }
}
