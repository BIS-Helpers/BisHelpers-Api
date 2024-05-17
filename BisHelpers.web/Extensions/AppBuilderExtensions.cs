namespace BisHelpers.web.Extensions;

public static class AppBuilderExtensions
{
    public static async Task<IApplicationBuilder> SeedUserAndRoles(this WebApplication app)
    {
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        await DefaultRoles.SeedAsync(scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>());
        await DefaultUsers.SeedAdminUserAsync(scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>());

        return app;
    }
}
