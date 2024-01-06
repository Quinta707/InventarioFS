using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Lotes;
using Academia.Inventario.API._Features.Productos;
using Academia.Inventario.API._Features.SalidaInventario;
using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.Inventario.API._Features.SalidasInventarioDetalles
{
    public class SalidasInventarioDetallesDomain
    {
        private readonly SalidasInventarioDomain _salidasInventarioDomain;
        private readonly LotesDomain             _lotesDomain;
        private readonly ProductosDomain         _productosDomain;

        public SalidasInventarioDetallesDomain(SalidasInventarioDomain salidasInventarioDomain, LotesDomain lotesDomain, ProductosDomain productosDomain)
        {
            _salidasInventarioDomain    = salidasInventarioDomain;
            _lotesDomain                = lotesDomain;
            _productosDomain            = productosDomain;
        }

        public bool SalidasInventarioDetalleExisteId(List<SalidasInventarioDetalle> salidasInventarioDetallesList, int? id)
        {
            if (salidasInventarioDetallesList.Where(x => x.SalidaInventarioDetalleId == id && x.Activo == true).Any())
                return true;
            return false;
        }

        public string AgregarDetalle(List<SalidasInventario> salidasList, int? salidasId, List<Lote> lotesList, int? productoID, int? cantidad, List<Producto> productoList)
        {

            if (!_salidasInventarioDomain.SalidasInventarioExisteId(salidasList, salidasId))
                return Mensajes.Registro_No_Existe("SalidaInventarioId");

            if (!_productosDomain.ProductoExisteId(productoList, productoID))
                return Mensajes.Registro_No_Existe("ProductoId");

            if (!lotesList.Where(x => x.ProductoId == productoID && x.CantidadActual >= cantidad).Any())
                return Mensajes.Inventario_Insuficiente;

            return "200";
        }

        public string EditarDetalle(List<SalidasInventario> salidasList, int? salidasId, List<Lote> lotesList, int? loteId, int? cantidad)
        {
            if (!_salidasInventarioDomain.SalidasInventarioExisteId(salidasList, salidasId))
                return Mensajes.Registro_No_Existe("SalidaInventarioId");

            if (!_lotesDomain.LotesExisteId(lotesList, loteId))
                return Mensajes.Registro_No_Existe("LoteId");

            return "200";
        }
    }
}
