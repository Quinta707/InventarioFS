using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.Roles
{
    public class RolesDomain
    {
        public bool RolesExisteId(List<Role> rolesList, int? id)
        {
            if (rolesList.Where(x => x.RolId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
