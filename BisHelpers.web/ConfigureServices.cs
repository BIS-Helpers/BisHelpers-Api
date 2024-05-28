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
        services.AddExceptionHandler<DefaultExceptionHandler>();
        services.AddCors(options =>
        {
            options.AddPolicy("Restricted", options =>
            {
                options
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>() ?? ["*"])
                    .AllowCredentials();
            });

            options.AddPolicy("AllowAll", options =>
            {
                options
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
            });
        });

        services.Configure<JWT>(configuration.GetSection("Authentication"));
        services.AddScoped<IUserClaimsPrincipalFactory<AppUser>, ApplicationUserClaimsPrincipalFactory>();
        services.AddValidatorsFromAssemblyContaining<Program>();

        return services;
    }
}
