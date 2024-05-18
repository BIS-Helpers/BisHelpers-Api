namespace BisHelpers.web.Extensions;

public static class EndPointExtensions
{
    public static RouteHandlerBuilder EndPointConfigurations(this RouteHandlerBuilder route,
        string Name,
        string version,
        string? description = null)
    {
        route.WithName(string.Join(' ', Name, version));

        if (description is not null)
            route.WithDescription(description);

        return route;
    }

    public static RouteHandlerBuilder UnauthorizedResponseConfiguration(this RouteHandlerBuilder route) =>
        route.Produces(StatusCodes.Status401Unauthorized);

    public static RouteHandlerBuilder OkResponseConfiguration(this RouteHandlerBuilder route) =>
        route.Produces(StatusCodes.Status200OK);
    public static RouteHandlerBuilder OkResponseConfiguration<T>(this RouteHandlerBuilder route) =>
        route.Produces<T>(StatusCodes.Status200OK);

    public static RouteHandlerBuilder CreatedResponseConfiguration(this RouteHandlerBuilder route) =>
        route.Produces(StatusCodes.Status201Created);

    public static RouteHandlerBuilder CreatedResponseConfiguration<T>(this RouteHandlerBuilder route) =>
        route.Produces<T>(StatusCodes.Status201Created);

    public static RouteHandlerBuilder ErrorResponseConfiguration(this RouteHandlerBuilder route, int ErrorDtoStatusCode, bool withBody = true) =>
        withBody ? route.Produces<ErrorDto>(ErrorDtoStatusCode) : route.Produces(ErrorDtoStatusCode);

}
