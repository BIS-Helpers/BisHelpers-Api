﻿namespace BisHelpers.Domain.Entities;

[Index(nameof(AcademicSemesterId), nameof(AcademicCourseId), nameof(ProfessorId), IsUnique = true)]
public class ProfessorAcademicCourse : BaseEntity
{
    #region Prperties
    public int Id { get; set; }
    #endregion

    #region Relations
    public AcademicCourse? AcademicCourses { get; set; }
    public int AcademicCourseId { get; set; }

    public AcademicSemester? AcademicSemester { get; set; }
    public int AcademicSemesterId { get; set; }

    public Professor? Professor { get; set; }
    public int ProfessorId { get; set; }

    public ICollection<AcademicLecture> AcademicLectures { get; set; } = [];
    #endregion
}
