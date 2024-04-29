namespace BisHelpers.web.Extensions;

public static class EndPointExtensions
{
    public static RouteHandlerBuilder EndPointConfigurations(this RouteHandlerBuilder route, string Name)
    {
        route.WithName(Name);

        return route;
    }

    public static RouteHandlerBuilder OkRouteConfiguration(this RouteHandlerBuilder route) =>
        route.Produces(200);

    public static RouteHandlerBuilder OkRouteConfiguration<T>(this RouteHandlerBuilder route) =>
        route.Produces<T>(200);

    public static RouteHandlerBuilder ErrorRouteConfiguration(this RouteHandlerBuilder route, int ErrorDtoStatusCode = 400) =>
        route.Produces<ErrorDto>(ErrorDtoStatusCode);
}
