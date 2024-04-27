using BisHelpers.Infrastructure.Repositories;

namespace BisHelpers.Infrastructure;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private readonly ApplicationDbContext _context = context;

    public IBaseRepository<Student> Students => new BaseRepository<Student>(_context);

    public async Task<int> CompleteAsync() =>
        await _context.SaveChangesAsync();
}
