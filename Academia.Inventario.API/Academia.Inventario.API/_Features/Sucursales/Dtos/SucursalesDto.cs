using Academia.Inventario.API._Common;
using System.ComponentModel.DataAnnotations;

namespace Academia.Inventario.API._Features.Sucursales.Dtos
{
    public class SucursalesDto
    {
        public int SucursalId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string? Nombre { get; set; }
    }
}
