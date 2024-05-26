using BisHelpers.Domain.Dtos.AcademicCourse;

namespace BisHelpers.Application.Services.AcademicCourse;
public class AcademicCourseService(IUnitOfWork unitOfWork) : IAcademicCourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Response<Domain.Entities.RelatedData.AcademicCourse>> AddProfessorAsync(ProfessorAcademicCourseDto dto, string userId)
    {
        var academicCourse = _unitOfWork.AcademicCourses.GetById(dto.AcademicCourseId);

        if (academicCourse is null)
        {
            return new Response<Domain.Entities.RelatedData.AcademicCourse>
            {
                ErrorBody = new ErrorBody
                {
                    Message = "Can not add professor to academic course",
                    Details = ["Academic course not found"]
                }
            };
        }

        var professorAcademicCourse = dto.MapToModel();

        professorAcademicCourse.CreatedById = userId;

        foreach (var lecture in professorAcademicCourse.AcademicLectures)
        {
            lecture.CreatedById = userId;
        }

        academicCourse.Professors.Add(professorAcademicCourse);
        await _unitOfWork.CompleteAsync();

        return new Response<Domain.Entities.RelatedData.AcademicCourse>
        {
            IsSuccess = true,
            Model = academicCourse
        };

    }

}
