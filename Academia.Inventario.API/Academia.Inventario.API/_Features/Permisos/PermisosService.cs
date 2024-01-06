using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Lotes.Dtos;
using Academia.Inventario.API._Features.Permisos.Dtos;
using Academia.Inventario.API._Features.Productos.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;
using static Academia.Inventario.API.Infrastructure.InventarioDB.Entities.Permiso;
using static Academia.Inventario.API.Infrastructure.InventarioDB.Entities.Producto;

namespace Academia.Inventario.API._Features.Permisos
{
    public class PermisosService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PermisosService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }

        public Respuesta<List<PermisosDto>> ListarPermisos()
        {
            var permisosList = (from permisos in _unitOfWork.Repository<Permiso>().AsQueryable()
                                where permisos.Activo == true
                                select new PermisosDto
                                {
                                    PermisoId = permisos.PermisoId,
                                    Nombre = permisos.Nombre,

                                }).ToList();

            return Respuesta.Success(permisosList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<PermisosDto> Insertar(PermisosDto permisosDto)
        {
            var permisosMap = _mapper.Map<Permiso>(permisosDto);

            permisosMap.UsuarioCreacionId = 1;
            permisosMap.FechaCreacion = DateTime.Now;
            permisosMap.UsuarioModificacionId = null;
            permisosMap.FechaModificacion = null;

            _unitOfWork.Repository<Permiso>().Add(permisosMap);
            _unitOfWork.SaveChanges();


            return Respuesta.Success(permisosDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<PermisosDto> Editar(PermisosDto permisosDto)
        {
            Permiso? permisosMap = _unitOfWork.Repository<Permiso>().FirstOrDefault(x => x.PermisoId == permisosDto.PermisoId);

            if (permisosMap == null)
                return Respuesta.Fault<PermisosDto>(Mensajes.Registro_No_Existe("ProductoId"), Codigos.BadRequest);

            permisosMap.PermisoId = permisosDto.PermisoId;
            permisosMap.Nombre = permisosDto.Nombre;
            permisosMap.UsuarioModificacionId = 1;
            permisosMap.FechaModificacion = DateTime.Now;

            _unitOfWork.SaveChanges();
            permisosMap.PermisoId = permisosDto.PermisoId;

            return Respuesta.Success(permisosDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
