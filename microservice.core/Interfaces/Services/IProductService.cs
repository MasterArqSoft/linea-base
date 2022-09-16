using microservice.core.DTOs.Product.Requests;
using microservice.core.DTOs.Product.Responses;
using microservice.domain.QueryFilters;
using microservice.domain.Wrappers;

namespace microservice.core.Interfaces.Services;

public interface IProductService
{
    PagedResponse<IEnumerable<ProductDtoResponse>> GetProducts(ProductQueryFilter filters, string actionUrl);

    Task<Response<ProductDtoResponse>> GetProductAsync(int id);

    Task<Response<ProductDtoResponse>> AddProductAsync(ProductAddDtoRequest product);

    Task<Response<ProductDtoResponse>> UpdateProductAsync(int id, ProductUpdDtoRequest product);

    Task<Response<bool>> DeleteProductAsync(int id);
}
