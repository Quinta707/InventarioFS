using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.Sucursales
{
    public class SucursalesDomain
    {
        public bool SucursalesExisteId(List<Sucursale> sucursalesList, int? id)
        {
            if (sucursalesList.Where(x => x.SucursalId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
