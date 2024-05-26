using BisHelpers.Application.Services.AcademicCourse;

namespace BisHelpers.Application;
public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IProfessorService, ProfessorService>();
        services.AddScoped<IAcademicCourseService, AcademicCourseService>();

        return services;
    }
}
