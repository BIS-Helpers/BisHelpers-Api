namespace BisHelpers.Infrastructure;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;
    private IDbContextTransaction? transaction;

    public IBaseRepository<Student> Students => new BaseRepository<Student>(_context);
    public IBaseRepository<Professor> Professors => new BaseRepository<Professor>(_context);
    public IBaseRepository<AcademicCourse> AcademicCourses => new BaseRepository<AcademicCourse>(_context);
    public IBaseRepository<AcademicLecture> AcademicLectures => new BaseRepository<AcademicLecture>(_context);
    public IBaseRepository<Semester> Semesters => new BaseRepository<Semester>(_context);
    public IBaseRepository<Announcement> Announcements => new BaseRepository<Announcement>(_context);
    public IBaseRepository<AcademicSemester> AcademicSemesters => new BaseRepository<AcademicSemester>(_context);

    public async Task BeginTransaction() =>
        transaction = await _context.Database.BeginTransactionAsync();

    public async Task TransactionCommit()
    {
        if (transaction is not null)
            await transaction.CommitAsync();
    }

    public async Task<int> CompleteAsync() =>
        await _context.SaveChangesAsync();
}
