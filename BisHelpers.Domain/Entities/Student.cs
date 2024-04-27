namespace BisHelpers.Domain.Entities;

public class Student : BaseEntity
{
    #region Prperties
    public int Id { get; set; }

    [StringLength(9)]
    public string CollegeId { get; set; } = null!;

    public DateTime DateOfJoin { get; set; }
    #endregion

    #region Relations
    public AppUser? User { get; set; }
    public string UserId { get; set; } = null!;
    #endregion 
}
