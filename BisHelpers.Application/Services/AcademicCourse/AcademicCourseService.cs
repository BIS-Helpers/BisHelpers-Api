using BisHelpers.Application.Services.AcademicSemester;

namespace BisHelpers.Application.Services.AcademicCourse;
public class AcademicCourseService(IUnitOfWork unitOfWork, IAcademicSemesterService academicSemesterService) : IAcademicCourseService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAcademicSemesterService _academicSemesterService = academicSemesterService;

    public async Task<Response<Domain.Entities.RelatedData.AcademicCourse>> AddProfessorAsync(CreateProfessorAcademicCourseDto dto, string userId)
    {
        var currentAcademicSemesterId = await _academicSemesterService.GetCurrentAcademicSemester();

        if (currentAcademicSemesterId is null)
        {
            return new Response<Domain.Entities.RelatedData.AcademicCourse>
            {
                ErrorBody = new ErrorBody
                {
                    Message = "Can not add professor to academic course",
                    Details = ["There is no active academic semester"]
                }
            };
        }

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
        professorAcademicCourse.AcademicSemesterId = (int)currentAcademicSemesterId;

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

    public async Task<IEnumerable<Domain.Entities.RelatedData.AcademicCourse>?> GetAll()
    {
        var courses = _unitOfWork.AcademicCourses.GetAll();

        if (courses is null)
            return null;

        return courses;
    }

    public async Task<Domain.Entities.RelatedData.AcademicCourse?> GetById(int id)
    {
        var courseQueryable = _unitOfWork.AcademicCourses.GetQueryable();

        var course = await courseQueryable
            .Include(c => c.Professors)
                .ThenInclude(p => p.CreatedBy)
            .Include(c => c.Professors)
                .ThenInclude(p => p.LastUpdatedBy)
            .Include(c => c.Professors)
                .ThenInclude(p => p.Professor)
            .Include(c => c.Professors)
                .ThenInclude(p => p.AcademicSemester)
            .SingleOrDefaultAsync(c => c.Id == id);

        if (course is null)
            return null;

        return course;
    }
}
