using BisHelpers.Application.Interfaces.Repositories;

namespace BisHelpers.Application.Interfaces;

public interface IUnitOfWork
{
    public IBaseRepository<Student> Students { get; }
    public IBaseRepository<Professor> Professors { get; }

    public Task BeginTransaction();

    public Task TransactionCommit();

    public Task<int> CompleteAsync();
}