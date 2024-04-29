namespace BisHelpers.Domain.Models;
public class JWT
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public double DurationInMin { get; set; }
}
