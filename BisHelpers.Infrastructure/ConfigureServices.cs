using BisHelpers.Application.Interfaces;

namespace BisHelpers.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContextCustom(connectionString);
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
