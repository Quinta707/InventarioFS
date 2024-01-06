using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Productos;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper.Internal;

namespace Academia.Inventario.API._Features.Lotes
{
    public class LotesDomain
    {
        private readonly ProductosDomain _productosDomain;

        public LotesDomain(ProductosDomain productosDomain)
        {
            _productosDomain = productosDomain;
        }

        public bool LotesExisteId(List<Lote> lotesList, int? id)
        {
            if (lotesList.Where(x => x.LoteId == id && x.Activo == true).Any())
                return true;
            return false;
        }

        public bool HayInventario(List<Lote> loteList, int? loteId, int? cantidad)
        {
            var filtrao = loteList.Where(x => x.LoteId == loteId && x.CantidadActual >= cantidad);

            if (filtrao.Count() < 1)
                return false;

            return true;
        }

        public string AgregarLote(List<Producto> productosList, int? id)
        {
            if (!_productosDomain.ProductoExisteId(productosList, id))
                return Mensajes.Registro_No_Existe("ProductoId");

            return "200";
        }
    }
}
