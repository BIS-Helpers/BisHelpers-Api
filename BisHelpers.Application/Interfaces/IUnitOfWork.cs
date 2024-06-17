using BisHelpers.Application.Interfaces.Repositories;

namespace BisHelpers.Application.Interfaces;

public interface IUnitOfWork
{
    public IBaseRepository<Student> Students { get; }
    public IBaseRepository<Professor> Professors { get; }
    public IBaseRepository<AcademicCourse> AcademicCourses { get; }
    public IBaseRepository<AcademicLecture> AcademicLectures { get; }
    public IBaseRepository<Semester> Semesters { get; }
    public IBaseRepository<Announcement> Announcements { get; }
    public IBaseRepository<AcademicSemester> AcademicSemesters { get; }

    public Task BeginTransaction();

    public Task TransactionCommit();

    public Task<int> CompleteAsync();
}