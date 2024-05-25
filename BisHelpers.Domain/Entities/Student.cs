namespace BisHelpers.Domain.Entities;

[Index(nameof(CollegeId), IsUnique = true)]
public class Student : BaseEntity
{
    #region Prperties
    public int Id { get; set; }

    [StringLength(9)]
    public string CollegeId { get; set; } = null!;

    public DateOnly DateOfJoin { get; set; }
    #endregion

    #region Relations
    public AppUser? User { get; set; }
    public string UserId { get; set; } = null!;

    public ICollection<AcademicRegistration> AcademicLectures { get; set; } = [];
    #endregion 
}
