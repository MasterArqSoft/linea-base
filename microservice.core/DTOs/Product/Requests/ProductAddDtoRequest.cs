namespace microservice.core.DTOs.Product.Requests;

public class ProductAddDtoRequest
{
    public int CodigoProducto { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
