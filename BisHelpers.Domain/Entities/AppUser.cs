namespace BisHelpers.Domain.Entities;

public class AppUser : IdentityUser
{
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    public AppUser? CreatedBy { get; set; }
    public string? CreatedById { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();

    public AppUser? LastUpdatedBy { get; set; }
    public string? LastUpdatedById { get; set; }
    public DateTime? LastUpdatedOn { get; set; }

    public bool IsDeleted { get; set; }
}
