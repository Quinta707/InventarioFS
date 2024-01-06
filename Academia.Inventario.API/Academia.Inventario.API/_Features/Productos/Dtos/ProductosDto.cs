using System.ComponentModel.DataAnnotations;

namespace Academia.Inventario.API._Features.Productos.Dtos
{
    public class ProductosDto
    {
        public int ProductoId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string? Nombre { get; set; }
    }
}
