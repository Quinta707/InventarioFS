using System.ComponentModel.DataAnnotations;

namespace Academia.Inventario.API._Features.EstadoSalidas.Dtos
{
    public class EstadoSalidasDto
    {
        public int EstadoId { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        public string? Descripcion { get; set; }
    }
}
