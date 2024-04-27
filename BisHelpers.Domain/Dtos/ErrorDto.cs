namespace BisHelpers.Domain.Dtos;

public class ErrorDto
{
    public int StatusCode { get; set; }

    public string ErrorCode { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public string Details { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    public string? Suggestion { get; set; } = string.Empty;
}