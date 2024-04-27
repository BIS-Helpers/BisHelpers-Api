namespace BisHelpers.Application.Extensions;

public static class IdentityExtensions
{
    public static string ToCustomErrorString(this IdentityResult result)
    {
        var error = string.Empty;

        foreach (var identityError in result.Errors)
            error += $"{identityError.Description},";

        return error;
    }
}
