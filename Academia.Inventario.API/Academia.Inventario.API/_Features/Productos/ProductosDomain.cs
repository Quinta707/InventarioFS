using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;

namespace Academia.Inventario.API._Features.Productos
{
    public class ProductosDomain
    {
        public bool ProductoExisteId(List<Producto> productosList, int? id)
        {
            if (productosList.Where(x => x.ProductoId == id && x.Activo == true).Any())
                return true;
            return false;
        }
    }
}
