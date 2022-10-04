using microservice.core.Interfaces.Repositories;
using microservice.domain.Entities;
using microservice.infrastructure.Setting;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace microservice.infrastructure.Repositories.RespositoryAsync;

public class ProductRepositoryAsync : GenericRepository<Product>, IProductRepository
{
    public ProductRepositoryAsync(MicroServiceContext microServiceContext) : base(microServiceContext)
    {

    }

    public async Task<Product> GetNombreProducto(bool activo, string nombreProducto) 
    {
        return (await _entities.Where(x => x.Activo.Equals(activo) && x.Nombre == nombreProducto)
                              .FirstOrDefaultAsync())!;
    }
}