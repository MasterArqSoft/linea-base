using microservice.domain.Entities;

namespace microservice.core.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetNombreProducto(bool activo, string nombreProducto);
    }
}
