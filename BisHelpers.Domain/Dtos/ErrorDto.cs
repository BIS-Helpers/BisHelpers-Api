using BisHelpers.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace BisHelpers.Domain.Dtos;

public class ErrorDto(HttpContext context)
{
    public int StatusCode { get; set; }
    public IEnumerable<ErrorBody?> Errors { get; set; } = [];
    public string RequestId { get; set; } = context.TraceIdentifier;
    public string Path { get; set; } = context.Request.Path;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}