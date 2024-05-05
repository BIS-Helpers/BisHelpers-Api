namespace BisHelpers.Domain.Models;
public class ErrorBody
{
    public string Code { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public IEnumerable<string> Details { get; set; } = [];
    public string Suggestion { get; set; } = "Please review the request and the documentation https://bishelpers.apidog.io/";
}
