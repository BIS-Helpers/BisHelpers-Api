using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.Application.Services.AcademicCourse;
public interface IAcademicCourseService
{
    public Task<Response<Domain.Entities.RelatedData.AcademicCourse>> AddProfessorAsync(ProfessorAcademicCourseDto dto, string userId);

}
