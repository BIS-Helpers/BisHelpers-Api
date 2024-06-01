using BisHelpers.Application.Services.AcademicCourse;
using BisHelpers.Application.Services.AcademicSemester;

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
