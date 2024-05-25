using BisHelpers.Application.Interfaces.Repositories;
using BisHelpers.Domain.Entities.RelatedData;

namespace BisHelpers.Application.Interfaces;

public interface IUnitOfWork
{
    public IBaseRepository<Student> Students { get; }
    public IBaseRepository<Professor> Professors { get; }
    public IBaseRepository<AcademicCourse> AcademicCourses { get; }
    public IBaseRepository<AcademicLecture> AcademicLectures { get; }

    public Task BeginTransaction();

    public Task TransactionCommit();

    public Task<int> CompleteAsync();
}