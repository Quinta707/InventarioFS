namespace Academia.Inventario.API._Features.RolesPorPermisos.Dtos
{
    public class RolesPorPermisosListDto
    {
        public int RolPorPermisoId { get; set; }

        public int? RolId { get; set; }

        public string? RolDescripcion { get; set; }

        public int? PermisoId { get; set; }

        public string? PermisoDescripcion { get; set; }
    }
}
