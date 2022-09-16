using microservice.domain.Entities;
using microservice.infrastructure.Setting;

namespace microservice.infrastructure.Repositories.RespositoryAsync;

public class ProductRepositoryAsync : GenericRepository<Product>
{
    public ProductRepositoryAsync(MicroServiceContext microServiceContext) : base(microServiceContext)
    {

    }
}
