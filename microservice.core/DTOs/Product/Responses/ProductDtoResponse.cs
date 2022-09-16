namespace microservice.core.DTOs.Product.Responses;

public class ProductDtoResponse
{
    public int CodigoProducto { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
}
