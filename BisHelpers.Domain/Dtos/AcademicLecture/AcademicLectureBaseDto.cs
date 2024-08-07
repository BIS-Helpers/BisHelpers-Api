﻿namespace BisHelpers.Domain.Dtos.AcademicLecture;
public class AcademicLectureBaseDto : BaseDto
{
    public int Id { get; set; }

    public string GroupNumber { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public string Day { get; set; } = null!;

    public string Year { get; set; } = null!;

    public string Semester { get; set; } = null!;
}
