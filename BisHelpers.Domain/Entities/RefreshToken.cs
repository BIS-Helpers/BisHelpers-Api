namespace BisHelpers.Domain.Entities;
public class RefreshToken
{
    #region Prperties
    public int Id { get; set; }
    public string Token { get; set; } = null!;
    public DateTime ExpiresOn { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? RevokedOn { get; set; }
    public bool IsActive => RevokedOn == null && !IsExpired;
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    #endregion

    #region Relations
    public AppUser? User { get; set; }
    public string UserId { get; set; } = null!;
    #endregion
}
