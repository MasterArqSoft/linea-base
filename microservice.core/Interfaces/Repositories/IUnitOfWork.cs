using microservice.domain.Entities;

namespace microservice.core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<Product> ProductRepositoryAsync { get; }

    Task BeginTransactionAsync();

    Task CommitAsync();

    Task RollbackAsync();
}
