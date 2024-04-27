namespace BisHelpers.Application.Services.Auth;

public class AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IStudentService studentService) : IAuthService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IStudentService _studentService = studentService;

    public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not null)
            return (IsSuccess: false, ErrorMessage: "Email is already registered!");

        var newUser = model.MapToAppUser();

        var createUserResult = await _userManager.CreateAsync(newUser, model.Password);

        if (!createUserResult.Succeeded)
            return (IsSuccess: false, ErrorMessage: createUserResult.ToCustomErrorString());

        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, AppRoles.Student);

        if (!addToRoleResult.Succeeded)
            return (IsSuccess: false, ErrorMessage: addToRoleResult.ToCustomErrorString());

        var createStudentResult = await _studentService.CreateAsync(model, newUser.Id);

        if (!createStudentResult.IsSuccess)
            return (IsSuccess: false, ErrorMessage: null);

        return (IsSuccess: true, ErrorMessage: null);
    }
}
