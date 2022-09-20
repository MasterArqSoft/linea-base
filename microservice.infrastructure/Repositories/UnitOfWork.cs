using microservice.core.Interfaces.Repositories;
using microservice.domain.Entities;
using microservice.infrastructure.Repositories.RespositoryAsync;
using microservice.infrastructure.Setting;
using Microsoft.EntityFrameworkCore.Storage;

namespace microservice.infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MicroServiceContext _context;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(MicroServiceContext context) => _context = context;

    public IGenericRepository<Product> ProductRepositoryAsync => new ProductRepositoryAsync(_context);
    public IGenericRepository<LogDetail> LogRepositoryAsync => new LogRepositoryAsync(_context);
    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
    }

    public async Task CommitAsync()
    {
        try
        {
            await BeginTransactionAsync();
            await _context.SaveChangesAsync();
            await _transaction!.CommitAsync();
        }
        catch
        {
            await RollbackAsync();
            throw;
        }
        finally
        {
            _transaction?.Dispose();
            Dispose();
        }
    }

    private bool disposed = false;

    public void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (!disposing)
            {
            }
            else
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public Task RollbackAsync()
    {
        _transaction?.Rollback();
        _transaction?.Dispose();
        return Task.CompletedTask;
    }
}
