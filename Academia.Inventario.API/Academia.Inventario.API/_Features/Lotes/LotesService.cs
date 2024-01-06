using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.EstadosCiviles.Dtos;
using Academia.Inventario.API._Features.Lotes.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;
using static Academia.Inventario.API.Infrastructure.InventarioDB.Entities.Lote;

namespace Academia.Inventario.API._Features.Lotes
{
    public class LotesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly LotesDomain _lotesDomain;

        public LotesService(IMapper mapper, UnitOfWorkBuilder unitOfWork, LotesDomain lotesDomain)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
            _lotesDomain = lotesDomain;
        }

        public Respuesta<List<LotesListDto>> ListarLotes()
        {
            var lotesList = (from lotes     in _unitOfWork.Repository<Lote>().AsQueryable()
                             join productos in _unitOfWork.Repository<Producto>().AsQueryable()
                             on   lotes.ProductoId equals productos.ProductoId
                                     where lotes.Activo == true
                                     select new LotesListDto
                                     {
                                         LoteId                 = lotes.LoteId, 
                                         ProductoId             = lotes.ProductoId,
                                         ProductoDescripcion    = productos.Nombre,
                                         CantidadInicial        = lotes.CantidadInicial,
                                         CantidadActual         = lotes.CantidadActual,
                                         CostoUnitario          = lotes.CostoUnitario,
                                         FechaVencimiento       = lotes.FechaVencimiento,

                                     }).ToList();

            return Respuesta.Success(lotesList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<LotesDto> Insertar(LotesDto lotesDto)
        {
            var lotesMap = _mapper.Map<Lote>(lotesDto);

            var productosList   = _unitOfWork.Repository<Producto>().AsQueryable().ToList();
            string respuesta    = _lotesDomain.AgregarLote(productosList, lotesMap.ProductoId);

            if(respuesta != "200")
                return Respuesta.Fault<LotesDto>(respuesta, Codigos.Error);

            lotesMap.CantidadActual         = lotesDto.CantidadInicial;
            lotesMap.UsuarioCreacionId      = 1;
            lotesMap.FechaCreacion          = DateTime.Now.Date;
            lotesMap.UsuarioModificacionId  = null;
            lotesMap.FechaModificacion      = null;

            _unitOfWork.Repository<Lote>().Add(lotesMap);
            _unitOfWork.SaveChanges();
            _unitOfWork.Commit();
            return Respuesta.Success(lotesDto, Mensajes.Proceso_Exitoso, Codigos.Success);

        }

        public Respuesta<LotesDto> Editar(LotesDto lotesDto)
        {
            Lote? lotesMap = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.LoteId == lotesDto.LoteId);

            if (lotesMap == null)
                return Respuesta.Fault<LotesDto>(Mensajes.Registro_No_Existe("LoteId"), Codigos.BadRequest);

            lotesMap.CantidadActual         = lotesDto.CantidadActual;
            lotesMap.FechaVencimiento       = lotesDto.FechaVencimiento;
            lotesMap.CostoUnitario          = lotesDto.CostoUnitario;
            lotesMap.UsuarioModificacionId  = 1;
            lotesMap.FechaModificacion      = DateTime.Now;

            var productosList   = _unitOfWork.Repository<Producto>().AsQueryable().ToList();
            string respuesta    = _lotesDomain.AgregarLote(productosList, lotesMap.ProductoId);

            if (respuesta != "200")
                return Respuesta.Fault<LotesDto>(respuesta, Codigos.Error);

            _unitOfWork.SaveChanges();
            lotesMap.LoteId = lotesDto.LoteId;

            return Respuesta.Success(lotesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
