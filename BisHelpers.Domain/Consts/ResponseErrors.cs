using BisHelpers.Domain.Models;

namespace BisHelpers.Domain.Consts;
public static class ResponseErrors
{
    /// <summary>
    /// Email is already registered
    /// </summary>
    public static ErrorBody Email40010 = new()
    {
        Code = "40010",
        Message = "Email is already registered!",
    };

    /// <summary>
    /// create user error
    /// </summary>
    public static ErrorBody Identity40020(IdentityResult identityResult)
    {
        ErrorBody error = new()
        {
            Code = "40020",
            Message = "Unable to create User",
            Details = ToErrorList(identityResult),
        };

        return error;
    }

    /// <summary>
    /// add role to user error
    /// </summary>
    public static ErrorBody Identity40021(IdentityResult identityResult)
    {
        ErrorBody error = new()
        {
            Code = "40021",
            Message = "Unable to add role to user",
            Details = ToErrorList(identityResult),
        };

        return error;
    }

    /// <summary>
    /// update user error
    /// </summary>
    public static ErrorBody Identity40022(IdentityResult identityResult)
    {
        ErrorBody error = new()
        {
            Code = "40022",
            Message = "Unable to update user",
            Details = ToErrorList(identityResult),
        };

        return error;
    }

    public static ErrorBody Identity40026(IdentityResult identityResult)
    {
        ErrorBody error = new()
        {
            Code = "40026",
            Message = "Unable to reset password",
            Details = ToErrorList(identityResult),
        };

        return error;
    }

    /// <summary>
    /// email or password is incorrect!
    /// </summary>
    public static ErrorBody Identity40023 = new()
    {
        Code = "40023",
        Message = "Email or Password is incorrect!"
    };

    public static ErrorBody Identity40024 = new()
    {
        Code = "40024",
        Message = "Invalid Token",
    };

    public static ErrorBody Identity40025 = new()
    {
        Code = "40024",
        Message = "Inactive token!",
    };

    /// <summary>
    /// user not found error
    /// </summary>
    public static ErrorBody NotFound40040 = new()
    {
        Code = "40040",
        Message = "Can not found user",
    };

    private static IEnumerable<string> ToErrorList(IdentityResult result) =>
        result.Errors.Select(e => e.Description);
}
