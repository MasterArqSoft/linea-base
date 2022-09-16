using AutoMapper;
using microservice.core.DTOs.Product.Requests;
using microservice.core.DTOs.Product.Responses;
using microservice.core.Interfaces.Repositories;
using microservice.core.Interfaces.Services;
using microservice.domain.Exceptions;
using microservice.domain.Helpers;
using microservice.domain.Interfaces;
using microservice.domain.QueryFilters;
using microservice.domain.QueryFilters.Pagination;
using microservice.domain.Settings;
using microservice.domain.Wrappers;
using Microsoft.Extensions.Options;
using microservice.domain.Entities;

namespace microservice.core.Features.Products;

public class ProductServices : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;
    private readonly PaginationOptionsSetting _paginationOptions;
    public ProductServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IUriService uriService,
            IOptions<PaginationOptionsSetting> options
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _uriService = uriService;
        _paginationOptions = options.Value;
    }
    public PagedResponse<IEnumerable<ProductDtoResponse>> GetProducts(ProductQueryFilter filters, string actionUrl)
    {
        PaginationFilter validFilter = new(filters.PageNumber, filters.PageSize, _paginationOptions);
        IEnumerable<Product>? ProductsPagedData = (IEnumerable<Product>)_unitOfWork.ProductRepositoryAsync
                                                            .GetPagedElementsAsync(
                                                                                    validFilter.PageNumber,
                                                                                    validFilter.PageSize,
                                                                                    x => x.Id,
                                                                                    true).Result;

        if (!string.IsNullOrEmpty(filters.Nombre))
        {
            ProductsPagedData = ProductsPagedData.Where(x => x.Nombre == filters.Nombre);
        }

        var total = _unitOfWork.ProductRepositoryAsync.GetCountAsync().Result;

        PagedResponse<IEnumerable<ProductDtoResponse>> response = PaginationHelper.PadageCreateResponse<ProductDtoResponse, Product>(
                                                                ProductsPagedData.ToList(),
                                                                validFilter,
                                                                _paginationOptions,
                                                                total,
                                                                _uriService,
                                                                actionUrl,
                                                                _mapper
                                                           );
        return response;
    }
    public async Task<Response<ProductDtoResponse>> GetProductAsync(int id)
    {
        Product ProductBuscado = await _unitOfWork.ProductRepositoryAsync.GetByIdAsync(id).ConfigureAwait(false);
        if (ProductBuscado == null) { throw new CoreException("La información solicitada no exitosa."); }
        ProductDtoResponse ProductMap = _mapper.Map<ProductDtoResponse>(ProductBuscado);
        return new Response<ProductDtoResponse>(ProductMap) { Message = "La información solicitada ha sido exitosa." };
    }
    public async Task<Response<ProductDtoResponse>> AddProductAsync(ProductAddDtoRequest product)
    {
        Product ProductMap = _mapper.Map<Product>(product);
        await _unitOfWork.ProductRepositoryAsync.AddAsync(ProductMap).ConfigureAwait(false);
        await _unitOfWork.CommitAsync();
        ProductDtoResponse ProductCreado = _mapper.Map<ProductDtoResponse>(ProductMap);
        return new Response<ProductDtoResponse>(ProductCreado) { Message = $"El Producto {ProductCreado.Nombre} ha sido creado." };
    }
    public async Task<Response<ProductDtoResponse>> UpdateProductAsync(int id, ProductUpdDtoRequest product)
    {
        Product ProductBuscado = await _unitOfWork.ProductRepositoryAsync
                                                .GetFirstAsync(x => x.Id.Equals(id))
                                                .ConfigureAwait(false);
        if (ProductBuscado == null) { throw new CoreException("La información solicitada no exitosa."); }

        ProductBuscado.CodigoProducto = product.CodigoProducto;
        ProductBuscado.Descripcion = product.Descripcion;

        await _unitOfWork.ProductRepositoryAsync.UpdateAsync(ProductBuscado);
        await _unitOfWork.CommitAsync();
        ProductDtoResponse ProductActualizado = _mapper.Map<ProductDtoResponse>(ProductBuscado);

        return new Response<ProductDtoResponse>(ProductActualizado) { Message = $"El Producto {ProductActualizado.Nombre} ha sido actualizada." };
    }
    public async Task<Response<bool>> DeleteProductAsync(int id)
    {
        if (id <= 0) { throw new CoreException($"El valor del identificador id debe ser superior a cero(0)."); }
        bool ProductEliminado = await _unitOfWork.ProductRepositoryAsync.DeleteAsync(id).ConfigureAwait(false);
        if (!ProductEliminado) { throw new CoreException("El registro no ha sido Eliminado."); }
        await _unitOfWork.CommitAsync();
        return new Response<bool>(ProductEliminado) { Message = $"El registro solicitado ha sido eliminado." };
    }
}
