using Microsoft.AspNetCore.Http;

namespace BisHelpers.Application.Extensions;
public static class HttpExtensions
{
    public static HttpResponse SetRefreshTokenCookie(this HttpResponse response, DateTime expiresOn, string refreshToken)
    {

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = expiresOn,
            Secure = true,
            IsEssential = true,
            SameSite = SameSiteMode.None
        };

        response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

        return response;
    }
}
