namespace BisHelpers.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string? connectionString)
    {
        services.AddDbContextCustom(connectionString);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
