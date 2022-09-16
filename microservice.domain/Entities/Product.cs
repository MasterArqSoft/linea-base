using microservice.domain.BaseEntities;

namespace microservice.domain.Entities;

public class Product : EntityBase
{
    public Product()
    {

    }
    public int CodigoProducto { get; set; }
    public string? Nombre { get; set; }
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
