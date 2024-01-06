using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.RolesPorPermisos.Dtos;
using Academia.Inventario.API._Features.RolesPorPermisos;
using Academia.Inventario.API._Features.SalidasInventarioDetalles.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using Academia.Inventario.API._Features.Lotes;
using Academia.Inventario.API._Features.Lotes.Dtos;

namespace Academia.Inventario.API._Features.SalidasInventarioDetalles
{
    public class SalidasInventarioDetallesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly SalidasInventarioDetallesDomain _salidasInventarioDetallesDomain;
        private readonly LotesService _loteService;

        public SalidasInventarioDetallesService(IMapper                         mapper, 
                                                UnitOfWorkBuilder               unitOfWork, 
                                                SalidasInventarioDetallesDomain salidasInventarioDetallesDomain,
                                                LotesService                    lotesService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
            _salidasInventarioDetallesDomain = salidasInventarioDetallesDomain;
            _loteService = lotesService;
        }

        public Respuesta<List<SalidasInventarioDetallesListDto>> ListarSalidasInventarioDetalles()
        {
            var salidasinventariodetalleList = (from salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                join lotes in _unitOfWork.Repository<Lote>().AsQueryable()
                                                on salidasdetalles.LoteId equals lotes.LoteId
                                                join productos in _unitOfWork.Repository<Producto>().AsQueryable()
                                                on lotes.ProductoId equals productos.ProductoId
                                                where salidasdetalles.Activo == true
                                                select new SalidasInventarioDetallesListDto
                                                {
                                                    SalidaInventarioDetalleId = salidasdetalles.SalidaInventarioDetalleId,
                                                    SalidaInventarioId = salidasdetalles.SalidaInventarioId,
                                                    LoteId = salidasdetalles.LoteId,
                                                    FechaVencimiento = lotes.FechaVencimiento,
                                                    ProductoID = lotes.ProductoId,
                                                    ProductoNombre = productos.Nombre,
                                                    Cantidad = salidasdetalles.Cantidad,
                                                    CostoUnitario = lotes.CostoUnitario,
                                                    TotalDetalle = salidasdetalles.Cantidad * Convert.ToInt32(lotes.CostoUnitario),
                                                    
                                                }).ToList();
            return Respuesta.Success(salidasinventariodetalleList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<List<SalidasInventarioDetallesListDto>> ListarSalidasInventarioDetallesPorSalida(int? salidaId)
        {
            var salidasinventariodetalleList = (from salidasdetalles in _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable()
                                                join lotes in _unitOfWork.Repository<Lote>().AsQueryable()
                                                on salidasdetalles.LoteId equals lotes.LoteId
                                                join productos in _unitOfWork.Repository<Producto>().AsQueryable()
                                                on lotes.ProductoId equals productos.ProductoId
                                                where salidasdetalles.Activo == true && salidasdetalles.SalidaInventarioId == salidaId
                                                select new SalidasInventarioDetallesListDto
                                                {
                                                    SalidaInventarioDetalleId = salidasdetalles.SalidaInventarioDetalleId,
                                                    SalidaInventarioId = salidasdetalles.SalidaInventarioId,
                                                    LoteId = salidasdetalles.LoteId,
                                                    FechaVencimiento = lotes.FechaVencimiento,
                                                    ProductoID = lotes.ProductoId,
                                                    ProductoNombre = productos.Nombre,
                                                    Cantidad = salidasdetalles.Cantidad,
                                                    CostoUnitario = lotes.CostoUnitario,
                                                    TotalDetalle = salidasdetalles.Cantidad * Convert.ToInt32(lotes.CostoUnitario),
                                                    
                                                }).ToList();
            return Respuesta.Success(salidasinventariodetalleList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<SalidasInventarioDetallesDto> Insertar(SalidasInventarioDetallesDto salidaDetallesDto)
        {
            var salidasDetallesMap = _mapper.Map<SalidasInventarioDetalle>(salidaDetallesDto);

            var salidasList     = _unitOfWork.Repository<SalidasInventario>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            //var detallesList    = _unitOfWork.Repository<SalidasInventarioDetalle>().AsQueryable().Where(x => x.Activo == true && x.SalidaInventarioId == salidaDetallesDto.SalidaInventarioId).ToList();
            var lotesList       = _unitOfWork.Repository<Lote>().AsQueryable().Where(x => x.Activo == true).ToList();
            var productoList    = _unitOfWork.Repository<Producto>().AsQueryable().Where(x => x.Activo == true).ToList();

            string Validacion = _salidasInventarioDetallesDomain.AgregarDetalle(salidasList, salidaDetallesDto.SalidaInventarioId, lotesList, salidaDetallesDto.ProductoId, salidaDetallesDto.Cantidad, productoList);

            if (Validacion != "200")
                return Respuesta.Fault<SalidasInventarioDetallesDto>(Validacion.ToString(), Codigos.Error);

            var loteAdecuado = lotesList.Where(x => x.ProductoId == salidaDetallesDto.ProductoId && x.FechaVencimiento > DateTime.Today.Date).OrderBy(X => X.FechaVencimiento).FirstOrDefault();
            if (loteAdecuado == null)
                return Respuesta.Fault<SalidasInventarioDetallesDto>(Mensajes.Lotes_No_Disponibles, Codigos.Error);

            LotesDto lotesdto = new LotesDto
            {
                LoteId              = loteAdecuado.LoteId,
                ProductoId          = loteAdecuado.ProductoId,
                CostoUnitario       = loteAdecuado.CostoUnitario,
                CantidadInicial     = loteAdecuado.CantidadInicial,
                CantidadActual      = loteAdecuado.CantidadActual - salidaDetallesDto.Cantidad,
                FechaVencimiento    = loteAdecuado.FechaVencimiento
            };

            _loteService.Editar(lotesdto);

            salidasDetallesMap.LoteId                   = loteAdecuado.LoteId;
            salidasDetallesMap.UsuarioCreacionId        = 1;
            salidasDetallesMap.FechaCreacion            = DateTime.Now;
            salidasDetallesMap.UsuarioModificacionId    = null;
            salidasDetallesMap.FechaModificacion        = null;

            _unitOfWork.Repository<SalidasInventarioDetalle>().Add(salidasDetallesMap); 

            if(!_unitOfWork.SaveChanges())
                return Respuesta.Fault<SalidasInventarioDetallesDto>(Mensajes.Peticion_Fallida, Codigos.Error);

            return Respuesta.Success(salidaDetallesDto, Mensajes.Proceso_Exitoso, Codigos.Success);

        }


        public Respuesta<SalidasInventarioDetallesDto> Editar(SalidasInventarioDetallesDto salidaDetallesDto)
        {
            SalidasInventarioDetalle? salidasDetallesMap = _unitOfWork.Repository<SalidasInventarioDetalle>().FirstOrDefault(x => x.SalidaInventarioDetalleId == salidaDetallesDto.SalidaInventarioDetalleId);

            if (salidasDetallesMap == null)
                return Respuesta.Fault<SalidasInventarioDetallesDto>(Mensajes.Registro_No_Existe("SalidasInventarioDetalleId"), Codigos.BadRequest);

            salidasDetallesMap.SalidaInventarioDetalleId    = salidaDetallesDto.SalidaInventarioDetalleId;
            salidasDetallesMap.SalidaInventarioId           = salidaDetallesDto.SalidaInventarioId;
            salidasDetallesMap.LoteId                       = salidaDetallesDto.LoteId;
            salidasDetallesMap.Cantidad                     = salidaDetallesDto.Cantidad;
            salidasDetallesMap.UsuarioModificacionId        = 1;
            salidasDetallesMap.FechaModificacion            = DateTime.Now;

            var salidasList     = _unitOfWork.Repository<SalidasInventario>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            var lotesList       = _unitOfWork.Repository<Lote>().AsQueryable().AsQueryable().Where(x => x.Activo == true).ToList();
            string Validacion   = _salidasInventarioDetallesDomain.EditarDetalle(salidasList, salidaDetallesDto.SalidaInventarioId, lotesList, salidaDetallesDto.LoteId, salidaDetallesDto.Cantidad);

            if (Validacion != "200")
                return Respuesta.Fault<SalidasInventarioDetallesDto>(Validacion.ToString(), Codigos.Error);

            _unitOfWork.SaveChanges();
            salidasDetallesMap.SalidaInventarioDetalleId = salidaDetallesDto.SalidaInventarioDetalleId;

            return Respuesta.Success(salidaDetallesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

    }
}
