﻿namespace BisHelpers.Domain.Dtos;

public class ProfileDto
{
    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateTime BirthDate { get; set; }

    public string? Level { get; set; }

    public string? CollegeId { get; set; }
}
