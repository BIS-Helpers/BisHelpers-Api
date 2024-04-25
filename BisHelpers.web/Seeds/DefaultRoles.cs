namespace BisHelpers.web.Seeds;

public static class DefaultRoles
{
    public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = roleManager.Roles.Select(r => r.Name).ToList();

        if (!roles.Contains(AppRoles.Admin))
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Admin));

        if (!roles.Contains(AppRoles.Coordinator))
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Coordinator));

        if (!roles.Contains(AppRoles.Professor))
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Professor));

        if (!roles.Contains(AppRoles.Student))
            await roleManager.CreateAsync(new IdentityRole(AppRoles.Student));
    }
}