namespace BisHelpers.Infrastructure.Extensions;

public static class ConfigurationExtensions
{
    public static IServiceCollection AddDbContextCustom(this IServiceCollection service, string? connectionString)
    {
        service.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

        return service;
    }


}
