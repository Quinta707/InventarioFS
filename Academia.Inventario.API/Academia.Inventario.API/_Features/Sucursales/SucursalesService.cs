using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Productos.Dtos;
using Academia.Inventario.API._Features.Sucursales.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Inventario.API._Features.Sucursales
{
    public class SucursalesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SucursalesService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }

        public Respuesta<List<SucursalesDto>> ListarSucursales()
        {
            var sucursalesList = (from sucursales in _unitOfWork.Repository<Sucursale>().AsQueryable()
                                 where sucursales.Activo == true
                                 select new SucursalesDto
                                 {
                                     SucursalId = sucursales.SucursalId,
                                     Nombre = sucursales.Nombre,

                                 }).ToList();

            return Respuesta.Success(sucursalesList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<SucursalesDto> Insertar(SucursalesDto sucursalesDto)
        {
            var sucursalesMap = _mapper.Map<Sucursale>(sucursalesDto);

            sucursalesMap.UsuarioCreacionId = 1;
            sucursalesMap.FechaCreacion = DateTime.Now;
            sucursalesMap.UsuarioModificacionId = null;
            sucursalesMap.FechaModificacion = null;

            _unitOfWork.Repository<Sucursale>().Add(sucursalesMap);
            _unitOfWork.SaveChanges();

            return Respuesta.Success(sucursalesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<SucursalesDto> Editar(SucursalesDto sucursalesDto)
        {
            Sucursale? sucursalesMap = _unitOfWork.Repository<Sucursale>().FirstOrDefault(x => x.SucursalId == sucursalesDto.SucursalId);

            if (sucursalesMap == null)
                return Respuesta.Fault<SucursalesDto>(Mensajes.Registro_No_Existe("SucursalId"), Codigos.BadRequest);

            sucursalesMap.SucursalId = sucursalesDto.SucursalId;
            sucursalesMap.Nombre = sucursalesDto.Nombre;
            sucursalesMap.UsuarioModificacionId = 1;
            sucursalesMap.FechaModificacion = DateTime.Now;

            _unitOfWork.SaveChanges();
            sucursalesMap.SucursalId = sucursalesDto.SucursalId;

            return Respuesta.Success(sucursalesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
