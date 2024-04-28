namespace BisHelpers.Application.Services.Auth;

public class AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IStudentService studentService, IUnitOfWork unitOfWork) : IAuthService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IStudentService _studentService = studentService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<(bool IsSuccess, string? ErrorMessage)> RegisterAsync(RegisterDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not null)
            return (IsSuccess: false, ErrorMessage: "Email is already registered!");

        var newUser = model.MapToAppUser();

        await _unitOfWork.BeginTransaction();

        var createUserResult = await _userManager.CreateAsync(newUser, model.Password);

        if (!createUserResult.Succeeded)
            return (IsSuccess: false, ErrorMessage: createUserResult.ToCustomErrorString());

        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, AppRoles.Student);

        if (!addToRoleResult.Succeeded)
            return (IsSuccess: false, ErrorMessage: addToRoleResult.ToCustomErrorString());

        var createStudentResult = await _studentService.CreateAsync(model, newUser.Id);

        if (!createStudentResult.IsSuccess)
            return (IsSuccess: false, createStudentResult.ErrorMessage);

        await _unitOfWork.TransactionCommit();

        return (IsSuccess: true, ErrorMessage: null);
    }

    public async Task<(bool IsSuccess, ProfileDto? model, string? ErrorMessage)> GetProfileAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return (IsSuccess: false, model: null, ErrorMessage: "User Not Found!");

        var profileDto = user.MapToProfileDto();

        if (await _userManager.IsInRoleAsync(user, AppRoles.Student) && user.Student is not null)
        {
            profileDto.CollegeId = user.Student.CollegeId;
            profileDto.Level = user.Student.DateOfJoin.ToCollegeLevel();
        }

        return (IsSuccess: true, model: profileDto, ErrorMessage: null);
    }
}
