using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Lotes;
using Academia.Inventario.API._Features.Productos;
using Academia.Inventario.API._Features.SalidasDeInventario.Dtos;
using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;
using Academia.Inventario.API._Features.Sucursales;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace Academia.Inventario.API._Features.SalidaInventario
{
    public class SalidasInventarioDomain
    {
        private readonly LotesDomain        _lotesDomain;
        private readonly SucursalesDomain   _sucursalesDomain;
        private readonly ProductosDomain    _productosDomain;

        public SalidasInventarioDomain(LotesDomain lotesDomain, SucursalesDomain sucursalesDomain, ProductosDomain productosDomain)
        {
            _lotesDomain        = lotesDomain;
            _sucursalesDomain   = sucursalesDomain;
            _productosDomain    = productosDomain;
        }
        public bool SalidasInventarioExisteId(List<SalidasInventario> salidasInventarioList, int? id)
        {
            if (salidasInventarioList.Where(x => x.SalidaInventarioId == id && x.Activo == true).Any())
                return true;
            return false;
        }

        public string AgregarSalida(List<Sucursale> sucursalList, int? sucursalID, List<SalidasInventario> salidasList, SalidasInventarioDto salidasInventarioDto, int? usuarioID, List<Usuario> usuarioList)
        {
            if (!usuarioList.Where(x => x.UsuarioId == usuarioID && x.RolId == 1).Any())
                return Mensajes.Permisos_Insuficientes;

            if (!_sucursalesDomain.SucursalesExisteId(sucursalList, sucursalID))
                return Mensajes.Registro_No_Existe("SucursalID");

            var cantidadPendiente = salidasList.Where(x => x.SucursalId == salidasInventarioDto.SucursalId && x.EstadoId != 1).Sum(x => x.SalidasInventarioDetalles.Sum(e => e.Cantidad));
            
            if (cantidadPendiente + salidasInventarioDto.SalidasDetalles.Sum(d => d.Cantidad) > 5000)
                return Mensajes.Sucursal_Limite;

            return "200";
        }
        public string AgregarDetalle(List<SalidasInventario> salidaList, int? salidaID, List<Lote> lotesList, int? cantidad, List<Producto> productosList, int? ProductoID)
        {
            if(SalidasInventarioExisteId(salidaList, salidaID))
                return Mensajes.Registro_No_Existe("SalidaInventarioID");

            if (!_productosDomain.ProductoExisteId(productosList, ProductoID))
                return Mensajes.Registro_No_Existe("ProductoId");

            if(!lotesList.Where(x => x.ProductoId == ProductoID && x.CantidadActual >= cantidad).Any())
                return Mensajes.Inventario_Insuficiente;


            return "200";
        }
        //public string AgregarDetalle(List<SalidasInventario> salidaList, int? salidaID, List<Lote> lotesList, int? cantidad, List<Producto> productosList, int? ProductoID)
        //{
        //    if (!SalidasInventarioExisteId(salidaList, salidaID))
        //    {
        //        return Mensajes.Registro_No_Existe("SalidaInventarioID");
        //    }

        //    if (!_productosDomain.ProductoExisteId(productosList, ProductoID))
        //    {
        //        return Mensajes.Registro_No_Existe("ProductoId");
        //    }

        //    if (!lotesList.Where(x => x.ProductoId == ProductoID && x.CantidadActual >= cantidad).Any())
        //    {
        //        return Mensajes.Inventario_Insuficiente;
        //    }

        //    return "200";
        //}

    }
}
