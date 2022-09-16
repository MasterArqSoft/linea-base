using AutoMapper;
using microservice.core.DTOs.Product.Requests;
using microservice.core.DTOs.Product.Responses;
using microservice.domain.Entities;

namespace microservice.core.Mappings;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductAddDtoRequest, Product>();
        CreateMap<Product, ProductDtoResponse>();
    }
}
