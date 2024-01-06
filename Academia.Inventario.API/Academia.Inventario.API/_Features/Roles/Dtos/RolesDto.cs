using System.ComponentModel.DataAnnotations;

namespace Academia.Inventario.API._Features.Roles.Dtos
{
    public class RolesDto
    {
        public int RolId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        public string? Nombre { get; set; }

    }
}
