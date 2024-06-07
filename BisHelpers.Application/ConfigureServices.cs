using BisHelpers.Application.Services.AcademicCourseService;
using BisHelpers.Application.Services.AcademicSemesterService;
using BisHelpers.Application.Services.AuthService;
using BisHelpers.Application.Services.ProfessorService;
using BisHelpers.Application.Services.StudentService;

namespace BisHelpers.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<IAcademicCourseService, AcademicCourseService>();
        services.AddScoped<IAcademicSemesterService, AcademicSemesterService>();

        return services;
    }
}
