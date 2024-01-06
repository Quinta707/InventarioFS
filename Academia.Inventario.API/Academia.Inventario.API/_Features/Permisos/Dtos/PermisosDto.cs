using System.ComponentModel.DataAnnotations;

namespace Academia.Inventario.API._Features.Permisos.Dtos
{
    public class PermisosDto
    {
        public int PermisoId { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string? Nombre { get; set; }
    }
}
