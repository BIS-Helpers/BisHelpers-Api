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

    public static IEnumerable<string> ToErrorList(this IdentityResult result) =>
        result.Errors.Select(e => e.Description);
}
