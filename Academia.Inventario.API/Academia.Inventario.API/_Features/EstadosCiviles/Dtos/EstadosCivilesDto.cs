using System.ComponentModel.DataAnnotations;

namespace Academia.Inventario.API._Features.EstadosCiviles.Dtos
{
    public class EstadosCivilesDto
    {
        public int EstadoCivilId { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es requerido")]
        public string? Descripcion { get; set; }
    }
}
