using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.EstadoSalidas
{
    public class EstadoSalidasDomain
    {
        public bool EstadoSalidaExisteId(List<EstadoSalida> estadoSalidaList, int? id)
        {
            if (estadoSalidaList.Where(x => x.EstadoId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
