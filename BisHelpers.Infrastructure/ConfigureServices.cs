namespace BisHelpers.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RemoteAccessServerConnection");

        services.AddDbContextCustom(connectionString);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
