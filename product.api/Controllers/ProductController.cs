using microservice.core.DTOs.Product.Requests;
using microservice.core.Interfaces.Services;
using microservice.domain.QueryFilters;
using microservice.domain.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace product.api.Controllers;

[ApiController]
[Produces("application/json")]
[Route("api/Product")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService
;   public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>
    /// Obtiene un listado de productos.
    /// </summary>
    /// <remarks>
    /// De acuerdo a los filtros se obtiene un listado de productos.
    /// </remarks>
    /// <returns>Retorna un listado de productos solicitado.</returns>
    /// <param name="filters">Diferentes filtros para obtener el listado de productos.</param>
    /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
    /// <response code="200">OK. Devuelve la información solicitada.</response>
    /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
    [HttpGet("listado", Name = nameof(GetAllProduct))]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAllProduct([FromQuery] ProductQueryFilter filters)
    {
        var Products = _productService.GetProducts(filters, Url.RouteUrl(nameof(GetAllProduct))!.ToString()!)!;
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(Products.Meta));
        return Ok(Products);
    }

    /// <summary>
    /// Obtiene un los datos de un producto.
    /// </summary>
    /// <remarks>
    /// Los datos del producto se obtiene por su Id.
    /// </remarks>
    /// <returns>Retorna los datos del product solicitado.</returns>
    /// <param name="id">Identificador del objeto producto.</param>
    /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
    /// <response code="200">OK. Devuelve la información solicitada.</response>
    /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
    [HttpGet("product/{id:int}", Name = "productById")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        return Ok(await _productService.GetProductAsync(id).ConfigureAwait(false));
    }

    /// <summary>
    /// Agrega un nuevo producto.
    /// </summary>
    /// <remarks>
    /// Inserta los datos del producto.
    /// </remarks>
    /// <returns>Retorna los datos del producto agregado.</returns>
    /// <param name="product">El objeto producto.</param>
    /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
    /// <response code="200">OK. Devuelve la información solicitada.</response>
    /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
    [HttpPost("product")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Post([FromBody] ProductAddDtoRequest product)
    {
        return Ok(await _productService.AddProductAsync(product));
    }

    /// <summary>
    /// Actualiza el producto.
    /// </summary>
    /// <remarks>
    /// Actualiza los diferentes datos del producto.
    /// </remarks>
    /// <returns>Retorna el product actualizado</returns>
    /// <param name="id">El identificador del producto</param>
    /// <param name="product">Los datos del producto.</param>
    /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
    /// <response code="200">OK. Devuelve la información solicitada.</response>
    /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
    [HttpPut("product/{id:int}")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductUpdDtoRequest product)
    {
        return Ok(await _productService.UpdateProductAsync(id, product));
    }

    /// <summary>
    /// Elimina el producto.
    /// </summary>
    /// <remarks>
    /// Elimina los dastos del  producto po su id.
    /// </remarks>
    /// <returns>Retorna el Objeto producto solicitado</returns>
    /// <param name="id">El identificador del producto.</param>
    /// <response code="500">InternalServerError. Ha ocurrido una exception no controlada.</response>
    /// <response code="200">OK. Devuelve la información solicitada.</response>
    /// <response code="404">NotFound. No se ha encontrado la información solicitada.</response>
    [HttpDelete("product/{id:int}")]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        return Ok(await _productService.DeleteProductAsync(id));
    }
}
