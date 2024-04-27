namespace BisHelpers.Domain.Entities;

public class AppUser : IdentityUser
{
    #region Prperties
    [MaxLength(100)]
    public string FullName { get; set; } = null!;

    [StringLength(10)]
    public string Gender { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsPending { get; set; }
    #endregion

    #region Relations
    public AppUser? CreatedBy { get; set; }
    public string? CreatedById { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();

    public AppUser? LastUpdatedBy { get; set; }
    public string? LastUpdatedById { get; set; }
    public DateTime? LastUpdatedOn { get; set; }

    public ICollection<RefreshToken>? RefreshTokens { get; set; }

    public Student? Student { get; set; }
    #endregion
}
