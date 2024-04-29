namespace BisHelpers.Application.Extensions;

public static class UserExtensions
{
    public static string GetUserId(this ClaimsPrincipal user) =>
        user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    public static JwtSecurityToken CreateJwtToken(this AppUser user, IList<string>? userRoles, IList<Claim>? userClaims, JWT jwt)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwt.Issuer,
            audience: jwt.Audience,
            claims: CreateCustomClaims(user: user, userRoles: userRoles, userClaims: userClaims),
            expires: DateTime.UtcNow.AddMinutes(jwt.DurationInMin),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }

    private static IEnumerable<Claim> CreateCustomClaims(AppUser user, IList<string>? userRoles, IList<Claim>? userClaims)
    {
        var roleClaims = new List<Claim>();

        foreach (var role in userRoles ?? [])
            roleClaims.Add(new Claim("roles", role));

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Sub, user.Id)
        }.Union(userClaims ?? []).Union(roleClaims);

        return claims;
    }
}