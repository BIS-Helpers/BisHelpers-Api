namespace BisHelpers.web;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGenCustom();
        services.AddIdentityCustom();
        services.AddAuthenticationCustom(configuration);
        services.AddAuthorization();

        services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }
}
