﻿using BisHelpers.Domain.Dtos.Student;

namespace BisHelpers.Application.Services.StudentService;
public interface IStudentService
{
    public Task<Response> RegisterAcademicLecturesAsync(string userId, RegisterAcademicLecturesDto dto);

    public Task<Student?> GetStudentAsync(string userId);

    public Task<(bool IsSuccess, int? StudentId, string? ErrorMessage)> CreateAsync(RegisterDto model, string userId);
}
