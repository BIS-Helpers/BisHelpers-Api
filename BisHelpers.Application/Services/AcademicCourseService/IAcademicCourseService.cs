namespace BisHelpers.Application.Services.AcademicCourseService;
public interface IAcademicCourseService
{
    public Task<Response<AcademicCourse>> AddProfessorAsync(CreateProfessorAcademicCourseDto dto, string userId);

    public Task<IEnumerable<AcademicCourse>?> GetAll();

    public Task<AcademicCourse?> GetById(int id);
}
