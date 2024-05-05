using BisHelpers.web.RouteGroups.VersionOne;

namespace BisHelpers.web.RouteGroups;

public static class GroupVersionOne
{
    public static RouteGroupBuilder VersionOneGroup(this RouteGroupBuilder builder)
    {
        builder.MapGroup("/Auth").GroupAuth().WithTags("Authentication");

        return builder;
    }
}
