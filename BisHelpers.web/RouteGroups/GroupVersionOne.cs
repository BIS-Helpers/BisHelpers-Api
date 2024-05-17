using BisHelpers.web.RouteGroups.VersionOne;

namespace BisHelpers.web.RouteGroups;

public static class GroupVersionOne
{
    public static RouteGroupBuilder VersionOneGroup(this RouteGroupBuilder builder)
    {
        builder.MapGroup("/auth").GroupAuthVersionOne().WithTags("Authentication");
        builder.MapGroup("/professor").GroupProfessorVersionOne().WithTags("Professor");

        return builder;
    }
}
