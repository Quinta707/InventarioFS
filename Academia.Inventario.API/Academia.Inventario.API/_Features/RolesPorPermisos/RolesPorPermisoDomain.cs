using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Permisos;
using Academia.Inventario.API._Features.Roles;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.RolesPorPermisos
{
    public class RolesPorPermisoDomain
    {
        private readonly RolesDomain _rolesDomain;
        private readonly PermisosDomain _permisosDomain;

        public RolesPorPermisoDomain(RolesDomain rolesDomain, PermisosDomain permisosDomain)
        {
            _rolesDomain = rolesDomain;
            _permisosDomain = permisosDomain;
        }
        public bool RolesPorPermisoExisteId(List<RolesPorPermiso> rolesporpermisoList, int? id)
        {
            if (rolesporpermisoList.Where(x => x.RolPorPermisoId == id && x.Activo == true).Any())
                return true;
            return false;
        }

        public string AgregarRolesPorPermiso(List<Role> rolesList, int? rolId, List<Permiso> permisosList, int? permisoId)
        {
            if (!_rolesDomain.RolesExisteId(rolesList, rolId))
                return Mensajes.Registro_No_Existe("RolId");

            if (!_permisosDomain.PermisosExisteId(permisosList, permisoId))
                return Mensajes.Registro_No_Existe("PermisoId");

            return "200";
        }
    }
}
