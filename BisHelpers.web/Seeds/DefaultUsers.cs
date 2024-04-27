namespace BisHelpers.web.Seeds;

public static class DefaultUsers
{
    public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager)
    {
        AppUser admin = new()
        {
            UserName = "admin@bishelpers.com",
            Email = "admin@bishelpers.com",
            FullName = "Bis Helpers Admin",
            EmailConfirmed = true,
            BirthDate = DateTime.UtcNow
        };

        var user = await userManager.FindByEmailAsync(admin.Email);

        if (user is null)
        {
            await userManager.CreateAsync(admin, "BisHelpers@1234");
            await userManager.AddToRolesAsync(admin, [AppRoles.Admin, AppRoles.Coordinator, AppRoles.Professor]);
        }
    }
}