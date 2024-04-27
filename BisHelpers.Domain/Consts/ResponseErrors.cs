using BisHelpers.Domain.Dtos;

namespace BisHelpers.Domain.Consts;
public static class ResponseErrors
{
    public static ErrorDto Validation(string? details = null)
    {
        ErrorDto error = new()
        {
            StatusCode = 400,
            ErrorCode = "4001",

            Message = "The request payload contains invalid or missing data.",
            Details = details ?? string.Empty,

            Suggestion = "Please review the request and the documentation https://bishelpers.apidog.io/ and ensure that all required fields are provided and conform to the expected data format and validation rules.",
        };

        return error;
    }

    public static ErrorDto UnableToRegister(string? details = null)
    {
        ErrorDto error = new()
        {
            StatusCode = 400,
            ErrorCode = "4002",

            Message = "Unable to register the user",
            Details = details ?? string.Empty,

            Suggestion = "Please review the request and the documentation https://bishelpers.apidog.io/",
        };

        return error;
    }
}
