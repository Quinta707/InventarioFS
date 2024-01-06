using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.Permisos
{
    public class PermisosDomain
    {
        public bool PermisosExisteId(List<Permiso> permisosList, int? id)
        {
            if (permisosList.Where(x => x.PermisoId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
