namespace BisHelpers.Application.Services.Auth;

public class AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IStudentService studentService, IUnitOfWork unitOfWork, IOptions<JWT> jwt) : IAuthService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    private readonly IStudentService _studentService = studentService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly JWT _jwt = jwt.Value;

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

    public async Task<(bool IsSuccess, AuthDto? model, string? ErrorMessage)> GetTokenAsync(LoginDto model)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(r => r.Email == model.Email);

        var isCorrectPassword = user is not null && await _userManager.CheckPasswordAsync(user, model.Password);

        if (!isCorrectPassword || user!.IsDeleted)
            return (IsSuccess: false, model: null, ErrorMessage: "Email or Password is incorrect!");

        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var jwtSecurityToken = user.CreateJwtToken(userRoles, userClaims, _jwt);

        var authModel = new AuthDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = jwtSecurityToken.ValidTo,
        };

        var isRefreshTokenActive = user.RefreshTokens!.Any(t => t.IsActive);

        if (isRefreshTokenActive)
        {
            var activeRefreshToken = user.RefreshTokens!.FirstOrDefault(t => t.IsActive)!;
            authModel.RefreshToken = activeRefreshToken.Token;
            authModel.RefreshTokenExpiration = activeRefreshToken.ExpiresOn;
        }
        else
        {
            var refreshToken = await GenerateRefreshTokenAsync(user);
            authModel.RefreshToken = refreshToken.Token;
            authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
        }

        return (IsSuccess: true, model: authModel, ErrorMessage: null);
    }

    public async Task<(bool IsSuccess, AuthDto? model, string? ErrorMessage)> RefreshTokenAsync(string token)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));

        if (user is null)
            return (IsSuccess: false, model: null, ErrorMessage: "Invalid token!");

        var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);

        if (!refreshToken.IsActive)
            return (IsSuccess: false, model: null, ErrorMessage: "Inactive token!");

        refreshToken.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = await GenerateRefreshTokenAsync(user);

        user.RefreshTokens!.Add(newRefreshToken);
        await _userManager.UpdateAsync(user);

        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var jwtSecurityToken = user.CreateJwtToken(userRoles, userClaims, _jwt);

        var authModel = new AuthDto
        {
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = jwtSecurityToken.ValidTo,
            RefreshToken = newRefreshToken.Token,
            RefreshTokenExpiration = newRefreshToken.ExpiresOn
        };

        return (IsSuccess: true, model: authModel, ErrorMessage: null);
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(AppUser user)
    {
        var randomNumber = new byte[32];

        using (var generator = new RNGCryptoServiceProvider())
            generator.GetBytes(randomNumber);

        var refreshToken = new RefreshToken
        {
            Token = Convert.ToBase64String(randomNumber),
            ExpiresOn = DateTime.UtcNow.AddDays(10),
            CreatedOn = DateTime.UtcNow
        };

        user.RefreshTokens!.Add(refreshToken);
        await _userManager.UpdateAsync(user);

        return refreshToken;
    }
}
