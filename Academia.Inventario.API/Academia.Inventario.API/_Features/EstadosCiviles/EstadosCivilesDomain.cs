using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.EstadosCiviles
{
    public class EstadosCivilesDomain
    {
        public bool EstadoCivilExisteId(List<EstadosCivile> estadoList, int? id)
        {
            if (estadoList.Where(x => x.EstadoCivilId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
