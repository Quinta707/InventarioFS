namespace Academia.Inventario.API._Features.Usuarios.Dtos
{
    public class UsuariosDto
    {
        public int UsuarioId { get; set; }

        public string? Nombre { get; set; }

        public string? Clave { get; set; }

        public int? EmpleadoId { get; set; }

        public int? RolId { get; set; }

        public bool? Activo { get; set; }

        public int? UsuarioCreacionId { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public int? UsuarioModificacionId { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}
