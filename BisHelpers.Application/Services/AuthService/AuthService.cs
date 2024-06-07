namespace BisHelpers.Application.Services.AuthService;

public class AuthService(UserManager<AppUser> userManager, IUnitOfWork unitOfWork, IOptions<JWT> jwt) : IAuthService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly JWT _jwt = jwt.Value;

    public async Task<Response> RegisterAsync(RegisterDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user is not null)
            return new Response { ErrorBody = ResponseErrors.Email40010 };

        var newUser = model.MapToAppUser();

        await _unitOfWork.BeginTransaction();

        var createUserResult = await _userManager.CreateAsync(newUser, model.Password);
        if (!createUserResult.Succeeded)
            return new Response { ErrorBody = ResponseErrors.Identity40020(createUserResult) };

        var addToRoleResult = await _userManager.AddToRoleAsync(newUser, AppRoles.Student);
        if (!addToRoleResult.Succeeded)
            return new Response { ErrorBody = ResponseErrors.Identity40021(addToRoleResult) };

        newUser.Student = model.MapToStudent();
        newUser.Student.CreatedById = newUser.Id;

        var updateUserResult = await _userManager.UpdateAsync(newUser);
        if (!updateUserResult.Succeeded)
            return new Response { ErrorBody = ResponseErrors.Identity40022(updateUserResult) };

        await _unitOfWork.TransactionCommit();

        return new Response { IsSuccess = true };
    }

    public async Task<Response<ProfileDto>> GetProfileAsync(string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Student)
                .ThenInclude(u => u.AcademicLectures)
                    .ThenInclude(a => a.AcademicLecture)
                        .ThenInclude(a => a.ProfessorAcademicCourse)
                         .ThenInclude(p => p.Professor)
            .Include(u => u.Student)
                .ThenInclude(u => u.AcademicLectures)
                    .ThenInclude(a => a.AcademicLecture)
                        .ThenInclude(a => a.ProfessorAcademicCourse)
                         .ThenInclude(p => p.AcademicSemester)
                            .ThenInclude(a => a.Semester)
            .Include(u => u.Student)
                .ThenInclude(u => u.AcademicLectures)
                    .ThenInclude(a => a.AcademicLecture)
                        .ThenInclude(a => a.ProfessorAcademicCourse)
                         .ThenInclude(p => p.AcademicCourses)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return new Response<ProfileDto> { ErrorBody = ResponseErrors.NotFound40040 };

        var profileDto = user.MapToProfileDto();

        if (await _userManager.IsInRoleAsync(user, AppRoles.Student) && user.Student is not null)
        {
            profileDto.CollegeId = user.Student.CollegeId;
            profileDto.Level = user.Student.DateOfJoin.ToCollegeLevel();
        }

        return new Response<ProfileDto> { IsSuccess = true, Model = profileDto };
    }

    public async Task<Response<AuthDto>> GetTokenAsync(LoginDto model)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokens).FirstOrDefaultAsync(r => r.Email == model.Email);

        var isCorrectPassword = user is not null && await _userManager.CheckPasswordAsync(user, model.Password);

        if (!isCorrectPassword || user!.IsDeleted)
            return new Response<AuthDto> { ErrorBody = ResponseErrors.Identity40023 };

        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var jwtSecurityToken = user.CreateJwtToken(userRoles, userClaims, _jwt);

        var authModel = new AuthDto
        {
            FullName = user.FullName,
            Email = user.Email ?? string.Empty,
            Gender = user.Gender,
            Roles = userRoles,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            ExpiresOn = jwtSecurityToken.ValidTo,
            AcademicYear = DateTime.UtcNow.Year.GetAcademicYear(),
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

        return new Response<AuthDto> { IsSuccess = true, Model = authModel };
    }

    public async Task<Response<AuthDto>> RefreshTokenAsync(string token)
    {
        var user = await _userManager.Users.Include(u => u.RefreshTokens).SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));

        if (user is null)
            return new Response<AuthDto> { ErrorBody = ResponseErrors.Identity40024 };

        var refreshToken = user.RefreshTokens!.Single(t => t.Token == token);

        if (!refreshToken.IsActive)
            return new Response<AuthDto> { ErrorBody = ResponseErrors.Identity40025 };

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

        return new Response<AuthDto> { IsSuccess = true, Model = authModel };
    }

    public async Task<Response> ResetPasswordAsync(ResetPasswordDto model, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return new Response { ErrorBody = ResponseErrors.NotFound40040 };

        var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

        if (!changePasswordResult.Succeeded)
            return new Response { ErrorBody = ResponseErrors.Identity40020(changePasswordResult) };

        return new Response { IsSuccess = true };
    }

    public async Task<Response> UpdateProfileAsync(ProfileUpdateDto model, string userId)
    {
        var user = await _userManager.Users
            .Include(u => u.Student)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user is null)
            return new Response { ErrorBody = ResponseErrors.NotFound40040 };

        user.FullName = model.FullName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;
        user.Gender = model.Gender;
        user.BirthDate = model.BirthDate;

        var updateResult = await _userManager.UpdateAsync(user);

        if (!updateResult.Succeeded)
            return new Response { ErrorBody = ResponseErrors.Identity40022(updateResult) };

        return new Response { IsSuccess = true };
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
