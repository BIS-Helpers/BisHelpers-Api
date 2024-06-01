namespace BisHelpers.Application.Services.AcademicCourse;
public interface IAcademicCourseService
{
    public Task<Response<Domain.Entities.RelatedData.AcademicCourse>> AddProfessorAsync(CreateProfessorAcademicCourseDto dto, string userId);

    public Task<IEnumerable<Domain.Entities.RelatedData.AcademicCourse>?> GetAll();

    public Task<Domain.Entities.RelatedData.AcademicCourse?> GetById(int id);
}
