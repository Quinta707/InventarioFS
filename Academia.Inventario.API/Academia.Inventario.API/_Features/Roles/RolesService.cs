using Academia.Inventario.API._Common;
using Academia.Inventario.API._Features.Permisos.Dtos;
using Academia.Inventario.API._Features.Roles.Dtos;
using Academia.Inventario.API.Infrastructure;
using Academia.Inventario.API.Infrastructure.InventarioDB.Entities;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace Academia.Inventario.API._Features.Roles
{
    public class RolesService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public RolesService(IMapper mapper, UnitOfWorkBuilder unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork.BuilderProyectoInventario();
        }

        public Respuesta<List<RolesDto>> ListarRoles()
        {
            var rolesList = (from roles in _unitOfWork.Repository<Role>().AsQueryable()
                             where roles.Activo == true
                             select new RolesDto
                             {
                                 RolId = roles.RolId,
                                 Nombre = roles.Nombre,

                             }).ToList();

            return Respuesta.Success(rolesList, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<RolesDto> Insertar(RolesDto rolesDto)
        {
            var rolesMap = _mapper.Map<Role>(rolesDto);

            rolesMap.UsuarioCreacionId = 1;
            rolesMap.FechaCreacion = DateTime.Now;
            rolesMap.UsuarioModificacionId = null;
            rolesMap.FechaModificacion = null;

            _unitOfWork.Repository<Role>().Add(rolesMap);
            _unitOfWork.SaveChanges();


            return Respuesta.Success(rolesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }

        public Respuesta<RolesDto> Editar(RolesDto rolesDto)
        {
            Role? rolesMap = _unitOfWork.Repository<Role>().FirstOrDefault(x => x.RolId == rolesDto.RolId);

            if (rolesMap == null)
                return Respuesta.Fault<RolesDto>(Mensajes.Registro_No_Existe("ProductoId"), Codigos.BadRequest);

            rolesMap.RolId = rolesDto.RolId;
            rolesMap.Nombre = rolesDto.Nombre;
            rolesMap.UsuarioModificacionId = 1;
            rolesMap.FechaModificacion = DateTime.Now;

            _unitOfWork.SaveChanges();
            rolesMap.RolId = rolesDto.RolId;

            return Respuesta.Success(rolesDto, Mensajes.Proceso_Exitoso, Codigos.Success);
        }
    }
}
