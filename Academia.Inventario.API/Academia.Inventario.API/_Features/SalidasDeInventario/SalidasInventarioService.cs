using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Lotes;
using Academia.Inventario.API._Features.Lotes.Dtos;
using Academia.Inventario.API._Features.SalidaInventario;
using Academia.Inventario.API._Features.SalidasDeInventario.Dtos;
using Academia.Inventario.API._Features.SalidasInventarioDetalles;
using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Inventario.API._Features.SalidasDeInventario
{
    public class SalidasInventarioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SalidasInventarioDomain _salidasInventarioDomain;
        private readonly LotesService _lotesService;

        public SalidasInventarioService(IMapper mapper, UnitOfWorkBuilder unitOfWork, SalidasInventarioDomain salidasInventarioDomain, LotesService lotesService)
        {
            _mapper                     = mapper;
            _unitOfWork                 = unitOfWork.BuilderProyectoInventario();
            _salidasInventarioDomain    = salidasInventarioDomain;
            _lotesService               = lotesService;
        }

        public Respuesta<List<SalidasInventarioListDto>> ListarSalidasInventario()
        {
            var salidasList = (from salidas  in _unitOfWork.Repository<SalidasInventario>().AsQueryable()
                              join  sucursal in _unitOfWork.Repository<Sucursale>().AsQueryable()
                              on    salidas.SucursalId equals sucursal.SucursalId
                              where salidas.Activo == true
                              select new SalidasInventarioListDto
                              {
                                  SalidaInventarioId        = salidas.SalidaInventarioId,
                                  SucursalId                = salidas.SucursalId,
                                  SucursalNombre            = sucursal.Nombre,
                                  FechaSalida               = salidas.FechaSalida,
                                  Total                     = (int?)(from   salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                                     join   lotes           in _unitOfWork.Repository<Lote>().AsQueryable()
                                                                     on     salidasdetalles.LoteId equals lotes.LoteId
                                                                     where  salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                                     select salidasdetalles.Cantidad * lotes.CostoUnitario).Sum(),
                                  UsuarioRecibeId           = salidas.UsuarioRecibeId,
                                  FechaRecibido             = salidas.FechaRecibido,
                                  SalidasInventarioDetalles = (from  salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                               join  lotes           in _unitOfWork.Repository<Lote>().AsQueryable()
                                                               on    salidasdetalles.LoteId equals lotes.LoteId
                                                               join  productos       in _unitOfWork.Repository<Producto>().AsQueryable()
                                                               on    lotes.ProductoId equals productos.ProductoId
                                                               where salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                               && salidasdetalles.Activo == true
                                                               select new SalidasInventarioDetallesListDto
                                                               {
                                                                   SalidaInventarioDetalleId    = salidasdetalles.SalidaInventarioDetalleId,
                                                                   SalidaInventarioId           = salidasdetalles.SalidaInventarioId,
                                                                   LoteId                       = salidasdetalles.LoteId,
                                                                   FechaVencimiento             = lotes.FechaVencimiento,
                                                                   ProductoID                   = lotes.ProductoId,
                                                                   ProductoNombre               = productos.Nombre,
                                                                   Cantidad                     = salidasdetalles.Cantidad,
                                                                   CostoUnitario                = lotes.CostoUnitario,
                                                                   TotalDetalle                 = salidasdetalles.Cantidad * Convert.ToInt32(lotes.CostoUnitario),
                                                                   
                                                               }).ToList(),

                              }).ToList();
            return Respuesta.Success(salidasList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<List<SalidasInventarioListDto>> ListarSalidasInventarioPorId(int? salidaId)
        {
            var salidasList = (from  salidas  in _unitOfWork.Repository<SalidasInventario>().AsQueryable()
                               join  sucursal in _unitOfWork.Repository<Sucursale>().AsQueryable()
                               on    salidas.SucursalId equals sucursal.SucursalId
                               where salidas.Activo == true && salidas.SalidaInventarioId == salidaId
                               select new SalidasInventarioListDto
                               {
                                   SalidaInventarioId        = salidas.SalidaInventarioId,
                                   SucursalId                = salidas.SucursalId,
                                   SucursalNombre            = sucursal.Nombre,
                                   FechaSalida               = salidas.FechaSalida,
                                   Total                     = (int?)(from   salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                                      join   lotes           in _unitOfWork.Repository<Lote>().AsQueryable()
                                                                      on     salidasdetalles.LoteId equals lotes.LoteId
                                                                      where  salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                                      select salidasdetalles.Cantidad * lotes.CostoUnitario).Sum(),
                                   UsuarioRecibeId           = salidas.UsuarioRecibeId,
                                   FechaRecibido             = salidas.FechaRecibido,
                                   SalidasInventarioDetalles = (from  salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                                join  lotes           in _unitOfWork.Repository<Lote>().AsQueryable()
                                                                on    salidasdetalles.LoteId equals lotes.LoteId
                                                                join  productos       in _unitOfWork.Repository<Producto>().AsQueryable()
                                                                on    lotes.ProductoId equals productos.ProductoId
                                                                where salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                               && salidasdetalles.Activo == true
                                                                select new SalidasInventarioDetallesListDto
                                                                {
                                                                    SalidaInventarioDetalleId   = salidasdetalles.SalidaInventarioDetalleId,
                                                                    SalidaInventarioId          = salidasdetalles.SalidaInventarioId,
                                                                    LoteId                      = salidasdetalles.LoteId,
                                                                    FechaVencimiento            = lotes.FechaVencimiento,
                                                                    ProductoID                  = lotes.ProductoId,
                                                                    ProductoNombre              = productos.Nombre,
                                                                    Cantidad                    = salidasdetalles.Cantidad,
                                                                    CostoUnitario               = lotes.CostoUnitario,
                                                                    TotalDetalle                = salidasdetalles.Cantidad * Convert.ToInt32(lotes.CostoUnitario),
                                                                   
                                                                }).ToList(),

                               }).ToList();
            return Respuesta.Success(salidasList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<List<SalidasInventarioListDto>> ListarSalidasInventarioPorSucursalId(int? sucursalId)
        {
            var salidasList = (from  salidas  in _unitOfWork.Repository<SalidasInventario>().AsQueryable()
                               join  sucursal in _unitOfWork.Repository<Sucursale>().AsQueryable()
                               on    salidas.SucursalId equals sucursal.SucursalId
                               where salidas.Activo == true && salidas.SucursalId == sucursalId
                               select new SalidasInventarioListDto
                               {
                                   SalidaInventarioId        = salidas.SalidaInventarioId,
                                   SucursalId                = salidas.SucursalId,
                                   SucursalNombre            = sucursal.Nombre,
                                   FechaSalida               = salidas.FechaSalida,
                                   Total                     = (int?)(from salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                                      join   lotes         in _unitOfWork.Repository<Lote>().AsQueryable()
                                                                      on     salidasdetalles.LoteId equals lotes.LoteId
                                                                      where  salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                                      select salidasdetalles.Cantidad * lotes.CostoUnitario).Sum(),
                                   UsuarioRecibeId           = salidas.UsuarioRecibeId,
                                   FechaRecibido             = salidas.FechaRecibido,
                                   SalidasInventarioDetalles = (from  salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                                join  lotes           in _unitOfWork.Repository<Lote>().AsQueryable()
                                                                on    salidasdetalles.LoteId equals lotes.LoteId
                                                                join  productos       in _unitOfWork.Repository<Producto>().AsQueryable()
                                                                on    lotes.ProductoId equals productos.ProductoId
                                                                where salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                               && salidasdetalles.Activo == true
                                                                select new SalidasInventarioDetallesListDto
                                                                {
                                                                    SalidaInventarioDetalleId   = salidasdetalles.SalidaInventarioDetalleId,
                                                                    SalidaInventarioId          = salidasdetalles.SalidaInventarioId,
                                                                    LoteId                      = salidasdetalles.LoteId,
                                                                    FechaVencimiento            = lotes.FechaVencimiento,
                                                                    ProductoID                  = lotes.ProductoId,
                                                                    ProductoNombre              = productos.Nombre,
                                                                    Cantidad                    = salidasdetalles.Cantidad,
                                                                    CostoUnitario               = lotes.CostoUnitario,
                                                                    TotalDetalle                = salidasdetalles.Cantidad * Convert.ToInt32(lotes.CostoUnitario),
                                                                    
                                                                }).ToList(),

                               }).ToList();

            return Respuesta.Success(salidasList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<List<ListadoEspecialDto>> ListarFormulario(DateTime? FechaInicio, DateTime? FechaFin, int? SucursalID)
        {
            var salidasList = (from  salidas  in _unitOfWork.Repository<SalidasInventario>().AsQueryable()
                               join  sucursal in _unitOfWork.Repository<Sucursale>().AsQueryable()
                               on    salidas.SucursalId equals sucursal.SucursalId
                               where salidas.Activo == true && salidas.FechaSalida >= FechaInicio && salidas.FechaSalida <= FechaFin && salidas.SucursalId == SucursalID 
                               select new ListadoEspecialDto
                               {
                                   SalidaInventarioId   = salidas.SalidaInventarioId,
                                   SucursalId           = salidas.SucursalId,
                                   SucursalNombre       = sucursal.Nombre,
                                   FechaSalida          = salidas.FechaSalida,
                                   CostoTotal           = (int?)(from salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                                 join   lotes         in _unitOfWork.Repository<Lote>().AsQueryable()
                                                                 on     salidasdetalles.LoteId equals lotes.LoteId
                                                                 where  salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId
                                                                 select salidasdetalles.Cantidad * lotes.CostoUnitario).Sum(),

                                   Unidades             = (from   salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                           join   lotes           in _unitOfWork.Repository<Lote>().AsQueryable()
                                                           on     salidasdetalles.LoteId equals lotes.LoteId
                                                                  
                                                           join   productos       in _unitOfWork.Repository<Producto>().AsQueryable()
                                                           on     lotes.ProductoId equals productos.ProductoId
                                                           where  salidasdetalles.SalidaInventarioId == salidas.SalidaInventarioId && salidasdetalles.Activo == true
                                                           select salidasdetalles.Cantidad).Sum(),
                                   UsuarioRecibeId      = salidas.UsuarioRecibeId,
                                   FechaRecibido    = salidas.FechaRecibido,

                               }).ToList();
            return Respuesta.Success(salidasList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<SalidasInventarioDto> InsertarSalidas(SalidasInventarioDto salidasInventarioDto)
        {
            var salidasMap     = _mapper.Map<SalidasInventario>(salidasInventarioDto);

            var sucursalesList = _unitOfWork.Repository<Sucursale>().AsQueryable().Where(x => x.Activo == true).ToList();

            var salidasList       = _unitOfWork.Repository<SalidasInventario>().AsQueryable().Where(x => x.Activo == true).ToList();
            var lotesList         = _unitOfWork.Repository<Lote>().AsQueryable().Where(x => x.Activo == true).ToList();
            var productosList     = _unitOfWork.Repository<Producto>().AsQueryable().Where(x => x.Activo == true).ToList();
            var detallesList      = _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable().Where(x => x.Activo == true).ToList();
            var usuariosList      = _unitOfWork.Repository<Usuario>().AsQueryable().Where(x => x.Activo == true).ToList();

            string Validacion     = _salidasInventarioDomain.AgregarSalida(sucursalesList, salidasInventarioDto.SucursalId, salidasList, salidasInventarioDto, salidasInventarioDto.UsuarioCreacionId, usuariosList);

            if (Validacion != "200")
                return Respuesta.Fault<SalidasInventarioDto>(Validacion.ToString(), Codigos.Error);

            salidasMap.UsuarioCreacionId        = 1;
            salidasMap.FechaCreacion            = DateTime.Now;
            salidasMap.UsuarioModificacionId    = null;
            salidasMap.FechaModificacion        = null;
            salidasMap.UsuarioRecibeId          = null;
            salidasMap.FechaRecibido            = null;

            foreach (var detalle in salidasInventarioDto.SalidasDetalles)
            {
                string ValidacionDetalle = _salidasInventarioDomain.AgregarDetalle(salidasList, salidasInventarioDto.SalidaInventarioId, lotesList, detalle.Cantidad, productosList, detalle.ProductoId);
                
                if (ValidacionDetalle != "200")
                    return Respuesta.Fault<SalidasInventarioDto>(ValidacionDetalle.ToString(), Codigos.Error);

                var loteAdecuado = lotesList.Where(x => x.ProductoId == detalle.ProductoId && x.FechaVencimiento > DateTime.Today.Date).OrderBy(X => X.FechaVencimiento).FirstOrDefault();
                if (loteAdecuado == null)
                    return Respuesta.Fault<SalidasInventarioDto>(Mensajes.Lotes_No_Disponibles, Codigos.Error);

                Lote lotes = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.LoteId == loteAdecuado.LoteId);
                lotes.CantidadActual = lotes.CantidadActual - detalle.Cantidad;
                _unitOfWork.Repository<Lote>().Update(lotes);
                detalle.SalidaInventarioId = salidasMap.SalidaInventarioId;

                var detalleSalida = _mapper.Map<SalidasInventarioDetalle>(detalle);

            }

            _unitOfWork.Repository<SalidasInventario>().Add(salidasMap);
            _unitOfWork.SaveChanges();

            return Respuesta.Success(salidasInventarioDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<SalidasInventarioDto> RecibirSalida(SalidasInventarioDto salidaDto)
        {
            SalidasInventario? salidasMap = _unitOfWork.Repository<SalidasInventario>().FirstOrDefault(x => x.SalidaInventarioId == salidaDto.SalidaInventarioId);

            if (salidasMap == null)
                return Respuesta.Fault<SalidasInventarioDto>(Mensajes.Registro_No_Existe("SalidasInventarioId"), Codigos.BadRequest);

            salidasMap.UsuarioRecibeId          = 1;
            salidasMap.FechaRecibido            = DateTime.Now.Date;
            salidasMap.UsuarioModificacionId    = 1;
            salidasMap.FechaModificacion        = DateTime.Now.Date;


            _unitOfWork.SaveChanges();
            salidasMap.SalidaInventarioId = salidaDto.SalidaInventarioId;

            return Respuesta.Success(salidaDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
