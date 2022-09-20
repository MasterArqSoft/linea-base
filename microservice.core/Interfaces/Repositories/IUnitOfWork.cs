using microservice.domain.Entities;

namespace microservice.core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Product> ProductRepositoryAsync { get; }
    IGenericRepository<LogDetail> LogRepositoryAsync { get; }
    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();
}
