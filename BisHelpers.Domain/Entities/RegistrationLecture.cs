namespace BisHelpers.Domain.Entities;

public class RegistrationLecture
{
    public AcademicRegistration? AcademicRegistration { get; set; }
    public int AcademicRegistrationId { get; set; }

    public AcademicLecture? AcademicLecture { get; set; }
    public int AcademicLectureId { get; set; }
}
