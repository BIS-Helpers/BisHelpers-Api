using BisHelpers.web.RouteGroups.VersionTwo;

namespace BisHelpers.web.RouteGroups;

public static class GroupVersionTwo
{
    public static RouteGroupBuilder VersionTwoGroup(this RouteGroupBuilder builder)
    {
        builder.MapGroup("/Auth").GroupAuthVersionTwo().WithTags("Authentication");

        return builder;
    }
}
